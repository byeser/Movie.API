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
        Task<FilmResponse> GetAsync(string title);
        Task<FilmResponse> GetAllAsync();
        Task AddAsync(Film film);
    }
}
