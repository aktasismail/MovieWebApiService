using AutoMapper;
using BusinessLayer.Abstact;
using DataAccessLayer.Abstract;
using EntitiesLayer;
using EntitiesLayer.DTO;
using EntitiesLayer.Exceptions;
using EntitiesLayer.RequestFeature;
using System.Dynamic;

namespace BusinessLayer.Concrete
{
    public class MovieManager : IMovieService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILogService _logService;
        private readonly IMapper _mapper;
        private readonly IMovieLinks _links;
        public MovieManager(IRepositoryManager manager, ILogService logService, IMapper mapper, IMovieLinks links)
        {
            _manager = manager;
            _logService = logService;
            _mapper = mapper;
            _links = links;
        }
        public async Task<MovieDTO> CreateMovie(MovieCreatedDto movies)
        {
            var entity = _mapper.Map<Movies>(movies);
            _manager.movieRepository.CreateMovie(entity);
            await _manager.Save();
            return _mapper.Map<MovieDTO>(entity);
        }
        public async Task DeleteMovie(int id, bool trackChanes)
        {
            var entity = await _manager.movieRepository.GetaMovie(id, trackChanes);
            if (entity is null)
            {
                throw new MovieNotFoundException(id);
            }
            _manager.movieRepository.Delete(entity);
            await _manager.Save();
        }
        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetAllMovie(LinkParameterDto linkParameterDto ,bool trackChanges)
        {
            var movie = await _manager.movieRepository.GetAllMovie(
                linkParameterDto.MovieParameter,trackChanges);
            var moviedto = _mapper.Map<IEnumerable<MovieDTO>>(movie);
            var links = _links.TryGenerateLinks(moviedto,
                linkParameterDto.MovieParameter.Fields,
                linkParameterDto.HttpContext);
            return (linkResponse: links, metaData: movie.MetaData);
        }
        public async Task<MovieDTO> GetaMovie(int id, bool trackChanges)
        {
            var entity = await _manager.movieRepository.GetaMovie(id, trackChanges);
            if (entity is null)
             throw new MovieNotFoundException(id);
            return _mapper.Map<MovieDTO>(entity);
        }
        public async Task UpdateMovie(int id, MovieUpdateDto movies, bool trackChanges)
        {
            var entity = await _manager.movieRepository.GetaMovie(id, trackChanges);
            if (entity is null)
                throw new MovieNotFoundException(id);
            entity = _mapper.Map<Movies>(movies);
            _manager.movieRepository.Update(entity);
            await _manager.Save();
        }
    }
}
