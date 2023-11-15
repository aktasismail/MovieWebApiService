using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRepositoryManager
    {
        IMovieRepository movieRepository { get; }
        Task Save();
    }
}
