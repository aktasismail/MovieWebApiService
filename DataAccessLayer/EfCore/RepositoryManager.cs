using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EfCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly MovieDbContext _context;
        private readonly Lazy<IMovieRepository> _movieRepository;
        public RepositoryManager(MovieDbContext? context)
        {
            _context = context;
            _movieRepository = new Lazy<IMovieRepository>(() => new MovieRepository(_context));
        }
        public IMovieRepository movieRepository => _movieRepository.Value;

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
