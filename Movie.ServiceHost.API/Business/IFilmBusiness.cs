using Movie.ServiceHost.API.Contracts;
using Movie.ServiceHost.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.ServiceHost.API.Business
{
    public interface IFilmBusiness
    {
        /// <summary>
        /// Get Title Films
        /// </summary>
        /// <param name="title">Filme Title</param>
        /// <returns>FilmReponse Class</returns>
        Task<FilmResponse> GetAsync(string title);
        /// <summary>
        /// Get All Films
        /// </summary>
        /// <returns>FilmReponse Class</returns>
        Task<FilmResponse> GetAllAsync();
        /// <summary>
        /// New Film Record
        /// </summary>
        /// <param name="film">Film Class</param>
        /// <returns>Task Film</returns>
        Task AddAsync(Film film);
        /// <summary>
        /// Film Update
        /// </summary>
        /// <returns>Task Film</returns>
        Task UpdateAsync();
    }
}
