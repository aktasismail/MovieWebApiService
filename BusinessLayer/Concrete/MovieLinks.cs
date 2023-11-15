using BusinessLayer.Abstact;
using EntitiesLayer;
using EntitiesLayer.DTO;
using EntitiesLayer.LinkModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MovieLinks : IMovieLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<MovieDTO> _shaper;
        public MovieLinks(LinkGenerator linkGenerator, IDataShaper<MovieDTO> shaper)
        {
            _linkGenerator = linkGenerator;
            _shaper = shaper;
        }
        public LinkResponse TryGenerateLinks(IEnumerable<MovieDTO> movieDTO, string fields, HttpContext httpContext)
        {
            var shappedmovie = ShapedData(movieDTO, fields);
            if (moviegeneratelinks(httpContext))
            {
                return ReturnLinkedMovies(movieDTO, fields, httpContext, shappedmovie);
            }
            return ReturnShapedMovies(shappedmovie);
        }

        private LinkResponse ReturnLinkedMovies(IEnumerable<MovieDTO> movieDTO, string fields, HttpContext httpContext, List<Entity> shappedmovie)
        {
            var movieDtoList = movieDTO.ToList();

            for (int index = 0; index < movieDtoList.Count(); index++)
            {
                var movieLinks = CreateForMovie(httpContext, movieDtoList[index], fields);
                shappedmovie[index].Add("Links", movieLinks);
            }

            var movieCollection = new LinkCollectionWrapper<Entity>(shappedmovie);
            CreateForMovies(httpContext, movieCollection);
            return new LinkResponse { Haslink = true, LinkedEntity = movieCollection };
        }
        private LinkCollectionWrapper<Entity> CreateForMovies(HttpContext httpContext,
           LinkCollectionWrapper<Entity> movieCollecionWrapper)
        {
            movieCollecionWrapper.Links.Add(new Links()
            {
                Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}",
                Rel = "self",
                Method = "GET"
            });
            return movieCollecionWrapper;
        }

        private List<Links> CreateForMovie(HttpContext httpContext,
            MovieDTO movieDTO,
            string fields)
        {
            var links = new List<Links>()
            {
               new Links()
               {
                   Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}" +
                   $"/{movieDTO.Id}",
                   Rel = "self",
                   Method = "GET"
                },
               new Links()
               {
                   Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}",
                   Rel="create",
                   Method = "POST"
               },
            };
            return links;
        }
        private LinkResponse ReturnShapedMovies(List<Entity> shappedmovie)
        {
            return new LinkResponse() { Entities = shappedmovie };
        }
        private bool moviegeneratelinks(HttpContext httpContext)
        {
            var mediatype = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];
            return mediatype.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.CurrentCultureIgnoreCase);
        }

        private List<Entity> ShapedData(IEnumerable<MovieDTO> movieDTO, string fields)
        {
            return _shaper.ShapeData(movieDTO, fields).Select(x => x.Entity).ToList();
        }
    }
}
