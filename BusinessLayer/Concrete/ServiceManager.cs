using AutoMapper;
using BusinessLayer.Abstact;
using DataAccessLayer.Abstract;
using EntitiesLayer;
using EntitiesLayer.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ServiceManager:IServiceManager
    {
        private readonly Lazy<IMovieService> _movieService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        public ServiceManager(IRepositoryManager repositorymanager,
            ILogService logService,
            IMapper mapper,
            IMovieLinks movieLinks,
            UserManager<User> userManager,
            IConfiguration configuration
            )
        {
            _movieService = new Lazy<IMovieService>(() => new MovieManager(repositorymanager,logService,mapper,movieLinks));
            _authenticationService = new Lazy<IAuthenticationService>(() =>
            new AuthenticationManager(logService, mapper, userManager, configuration));
        }
        public IMovieService MovieService => _movieService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
