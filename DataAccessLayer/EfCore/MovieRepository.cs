using DataAccessLayer.Abstract;
using EntitiesLayer;
using EntitiesLayer.RequestFeature;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EfCore
{
    public class MovieRepository : EntityRepositoryBase<Movies>, IMovieRepository
    {
        public MovieRepository(MovieDbContext context) : base(context)
        {   
        }
        public void CreateMovie(Movies movies) =>
            Create(movies);
        public void DeleteMovie(Movies movies) =>
            Delete(movies);
        public async Task<PageList<Movies>> GetAllMovie(MoviePageParameter moviePageParameter ,bool trackchanges)
        {
            var movies = await GetAll(trackchanges)
            .FilterMovie(moviePageParameter.MinRate,moviePageParameter.MaxRate)
            .Searching(moviePageParameter.SearchParameter)
            .Sort(moviePageParameter.OrderBy)
            .ToListAsync();
            return PageList<Movies>.ToPageList(movies, moviePageParameter.Pagesize, moviePageParameter.Pagenumber);
        }
           
        public async Task<Movies> GetaMovie(int id, bool trackChanges) =>
            await Get(x => x.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        public void UpdateMovie(Movies movies)=>
            Update(movies);
    }
}
