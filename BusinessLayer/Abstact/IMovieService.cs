using EntitiesLayer;
using EntitiesLayer.DTO;
using EntitiesLayer.RequestFeature;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstact
{
    public interface IMovieService
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllMovie(LinkParameterDto linkParameterDto ,bool trackChanges);
        Task<MovieDTO> GetaMovie(int id, bool trackChanges);
        Task<MovieDTO> CreateMovie(MovieCreatedDto movies);
        Task DeleteMovie(int id, bool trackChanes);
        Task UpdateMovie(int id,MovieUpdateDto movies, bool trackChanges);
    }
}
