using BusinessLayer.Abstact;
using EntitiesLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Filters;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController:ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public AccountController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost(Name = "RegisterUser")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            var result = await _serviceManager.AuthenticationService.RegisterUser(registerDto);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.TryAddModelError(item.Code, item.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);

        }
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginDto)
        {
            if (!await _serviceManager.AuthenticationService.ValidateUser(loginDto))
            {
                return Unauthorized();
            }
            return Ok(new
            {
                Token = await _serviceManager.AuthenticationService.CreateToken()   
            });
        }
    }
}
