using BusinessLayer.Abstact;
using EntitiesLayer;
using EntitiesLayer.DTO;
using EntitiesLayer.RequestFeature;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/movie")]
    public class MovieController : ControllerBase
    {
        private readonly IServiceManager _service;
        public MovieController(IServiceManager service)
        {
            _service = service;
        }
        //[HttpHead] BODY OLMADAN GÖSTERME
        [HttpGet(Name = "GetAllMovies")]
        [ServiceFilter(typeof(MediaTypeAttribute))]
        public async Task<IActionResult> GetAllMovies([FromQuery] MoviePageParameter moviePageParameter)
        {
            var linkparameter = new LinkParameterDto()
            {
                MovieParameter = moviePageParameter,
                HttpContext = HttpContext
            };
            var result = await _service.MovieService.GetAllMovie(linkparameter ,false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));
            return result.linkResponse.Haslink?
                Ok(result.linkResponse.LinkedEntity) :Ok(result.linkResponse.Entities);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMovie([FromRoute(Name ="id")] int id)
        {
            var entity = await _service.MovieService.GetaMovie(id, false);
            return Ok(entity);
        }
        [HttpPost(Name ="AddMovie")]
        public async Task<IActionResult> AddMovie([FromBody] MovieCreatedDto movies)
        {
            if (movies is null)
                return BadRequest();
            await _service.MovieService.CreateMovie(movies);
            return StatusCode(201,movies);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMovies([FromBody] MovieUpdateDto movies, [FromRoute(Name ="id")] int id)
        {
            if (movies is null)
                return BadRequest();
           await _service.MovieService.UpdateMovie(id,movies,true);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id)
        {
            await _service.MovieService.DeleteMovie(id,false);
            return NoContent();
        }
        [HttpOptions]
        public IActionResult GetMoviesOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, OPTIONS, HEAD");
            return Ok();
        }

    }
}
