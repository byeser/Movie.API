using Movie.ServiceHost.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.ServiceHost.API.Data
{
    public interface IFilmRepository
    {
        /// <summary>
        /// Get Title Film
        /// </summary>
        /// <param name="title">Film Title</param>
        /// <returns>Task<IEnumerable<Film>></returns>
        Task<IEnumerable<Film>> GetAsync(string title);
        /// <summary>
        /// Get All Film
        /// </summary>
        /// <returns>Task<IEnumerable<Film>></returns>
        Task<IEnumerable<Film>> GetAllAsync();
        /// <summary>
        /// New Record Film
        /// </summary>
        /// <param name="film">Film Class</param>
        /// <returns>Task Film</returns>
        Task AddAsync(Film film);
        /// <summary>
        /// Update Film
        /// </summary>
        /// <param name="film">Film Class</param>
        /// <returns>Task Film</returns>
        Task UpdateAsync(Film film);
    }
}
