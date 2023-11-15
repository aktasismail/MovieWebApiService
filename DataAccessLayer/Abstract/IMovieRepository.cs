using DataAccessLayer.EfCore;
using EntitiesLayer;
using EntitiesLayer.RequestFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IMovieRepository : IEntityRepositoryBase<Movies>
    {
        void CreateMovie(Movies movies);
        void DeleteMovie(Movies movies);
        Task<Movies> GetaMovie(int id, bool trackChanges);
        Task<PageList<Movies>> GetAllMovie(MoviePageParameter moviePageParameter, bool trackchanges);
        void UpdateMovie(Movies movies);
    }
}
