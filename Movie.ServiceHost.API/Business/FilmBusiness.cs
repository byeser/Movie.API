using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Movie.ServiceHost.API.Contracts;
using Movie.ServiceHost.API.Data;
using Movie.ServiceHost.API.Models;
using Movie.ServiceHost.API.ServiceAdapters;
using Newtonsoft.Json;

namespace Movie.ServiceHost.API.Business
{
    public class FilmBusiness : IFilmBusiness
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IOmbdService _ombdService;
        private readonly IMemoryCache _memoryCache;
        private const string cacheKey = "filmCacheSearch";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filmRepository">Film Repository</param>
        /// <param name="memoryCache">Memory Cache</param>
        public FilmBusiness(IFilmRepository filmRepository, IMemoryCache memoryCache)
        {
            _filmRepository = filmRepository;
            _memoryCache = memoryCache;
        }
        /// <summary>
        /// New Film Record
        /// </summary>
        /// <param name="film">Film Class</param>
        /// <returns>Task Film</returns>
        public async Task AddAsync(Film film)
        {
            Film f = new Film
            {
                imdbID = film.imdbID,
                poster = film.poster,
                title = film.title,
                type = film.type,
                year = film.year
            };
            await _filmRepository.AddAsync(f);
        }
        /// <summary>
        /// Get All Film List
        /// </summary>
        /// <returns>FilmReponse Class</returns>
        public async Task<FilmResponse> GetAllAsync()
        {

            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Film> film))
            {
                film = await _filmRepository.GetAllAsync();

                var cacheExpirationOptions =
                    new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(12),
                        Priority = CacheItemPriority.Normal
                    };
                _memoryCache.Set(cacheKey, film, cacheExpirationOptions);
            }

            FilmResponse response = new FilmResponse();
            if (film.ToList().Count() == 0)
                response.Message = "Film bulunamadı !";
            else
                response.Films.AddRange(film);

            return response;
        }
        /// <summary>
        ///  Get Title Films
        /// </summary>
        /// <param name="title">Film Title</param>
        /// <returns>FilmReponse Class</returns>
        public async Task<FilmResponse> GetAsync(string title)
        {
            IEnumerable<Film> film;

            film = (IEnumerable<Film>)_memoryCache.Get(cacheKey);
            film = film.Where(x => x.title.Contains(title)).ToList();

            if (film.Count() == 0)
            {
                film = await _filmRepository.GetAsync(title);
            }
            FilmResponse response = new FilmResponse();
            if (film.Count() == 0)
            {                
                var result = _ombdService.ValidateFilm(title);
                foreach (var item in result)
                {
                    await _filmRepository.AddAsync(item);
                }

                var cacheExpirationOptions =
                    new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(12),
                        Priority = CacheItemPriority.Normal
                    };
                _memoryCache.Remove(cacheKey);
                _memoryCache.Set(cacheKey,  await _filmRepository.GetAllAsync(), cacheExpirationOptions);
                response.Films.AddRange(result);
            }
            else
                response.Films.AddRange(film);
            return response;
            
        }
        /// <summary>
        /// 10 minutes update
        /// </summary>
        /// <returns>Task Film</returns>
        public async Task UpdateAsync()
        {
            IEnumerable<Film> films= await  _filmRepository.GetAllAsync();
            foreach (var item in films)
            {               
                foreach (var subItem in _ombdService.ValidateFilm(item.title))
                {
                    await _filmRepository.UpdateAsync(subItem);
                }
            }
        }
    }
}
