using Movie.ServiceHost.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.ServiceHost.API.Data
{
    public interface IFilmRepository
    {
        Task<IEnumerable<Film>> GetAsync(string title);
        Task<IEnumerable<Film>> GetAllAsync();
        Task AddAsync(Film film);
        Task UpdateAsync(Film film);
    }
}
