using EntitiesLayer.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api")]
    public class RootController:ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        public RootController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }
        [HttpGet(Name ="GetRoot")]
        public async Task<IActionResult> GetRoot([FromHeader(Name ="Accept")] string mediatype)
        {
            if (mediatype.Contains("application/ismailaktas.apiroot"))
            {
                var linklist = new List<Links>()
                {
                    new Links
                    {
                        Href=_linkGenerator.GetUriByName(HttpContext, nameof(GetRoot),new{}),
                        Rel="_self",
                        Method="GET",
                    },
                    new Links
                    {
                        Href=_linkGenerator.GetUriByName(HttpContext,nameof(MovieController.GetAllMovies),new{}),
                        Rel="movie",
                        Method="GET",
                    },
                    new Links
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext,nameof(MovieController.AddMovie),new{}),
                        Rel="movie",
                        Method="POST"
                    }
                };
                return Ok(linklist);
            }
            return NoContent();
        }

    }
}
