using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstact
{
    public interface IServiceManager
    {
        IMovieService MovieService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
