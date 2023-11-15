using AutoMapper;
using BusinessLayer.Abstact;
using EntitiesLayer;
using EntitiesLayer.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User? _user;
        public AuthenticationManager(ILogService logService, IMapper mapper,
            UserManager<User> userManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _logService = logService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<string> CreateToken()
        {
            var credentials = GetLoginCredential();
            var claims = await GetClaims();
            var tokenoptions = GenerateTokenOptions(credentials,claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenoptions);
        }
        public async Task<IdentityResult> RegisterUser(RegisterDto registerDto)
        {
            //var user = new User
            //{
            //    Name = registerDto.Name,
            //    Email = registerDto.Email,
            //    Surname= registerDto.Surname,
            //    PasswordHash= registerDto.Password,
            //    PhoneNumber = registerDto.PhoneNumber,
            //};
            var user = _mapper.Map<User>(registerDto);
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, registerDto.Roles);
            return result;
        }

        public async Task<bool> ValidateUser(LoginDto loginDto)
        {
            _user = await _userManager.FindByNameAsync(loginDto.UserName);
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user,loginDto.Password));
            if (!result)
            {
                _logService.LogWarning($"{nameof(ValidateUser)}: Kullanıcı adı veya şifre yanlış");
            }
            return result;
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private SigningCredentials GetLoginCredential()
        {
            var jwtSetting = _configuration.GetSection("JwtSetting");
            var key = Encoding.UTF8.GetBytes(jwtSetting["secretKey"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha512);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials, List<Claim> claims)
        {
            var jwtSetting = _configuration.GetSection("JwtSetting");
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSetting["validIssuer"],
                audience: jwtSetting["https://localhost:7042/"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );
            return tokenOptions;
        }
    }
}
