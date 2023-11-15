using EntitiesLayer;
using EntitiesLayer.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstact
{
    public interface IMovieLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<MovieDTO> movieDTOs, string fields, HttpContext httpContext);
    }
}
