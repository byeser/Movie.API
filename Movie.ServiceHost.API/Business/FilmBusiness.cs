using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Movie.ServiceHost.API.Contracts;
using Movie.ServiceHost.API.Data;
using Movie.ServiceHost.API.Models;
using Newtonsoft.Json;

namespace Movie.ServiceHost.API.Business
{
    public class FilmBusiness : IFilmBusiness
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IMemoryCache _memoryCache;
        private const string cacheKey = "filmCacheSearch";
        public FilmBusiness(IFilmRepository filmRepository, IMemoryCache memoryCache)
        {
            _filmRepository = filmRepository;
            _memoryCache = memoryCache;
        }
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
                var url = new WebClient().DownloadString("http://www.omdbapi.com/?apikey=1bdede5a&s=" + title);
                var data = JsonConvert.DeserializeObject<dynamic>(url);
                List<Film> films = new List<Film>();
                int counter = data.Search.Count;
                for (int i = 0; i < counter; i++)
                {
                    Film f = new Film()
                    {
                        imdbID = data.Search[i].imdbID.ToString(),
                        title = data.Search[i].Title.ToString(),
                        year = data.Search[i].Year.ToString(),
                        type = data.Search[i].Type.ToString(),
                        poster = data.Search[i].Poster.ToString()
                    };
                    films.Add(f);
                    await _filmRepository.AddAsync(f);

                }                
                var cacheExpirationOptions =
                    new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(12),
                        Priority = CacheItemPriority.Normal
                    };
                _memoryCache.Remove(cacheKey);
                _memoryCache.Set(cacheKey,  await _filmRepository.GetAllAsync(), cacheExpirationOptions);
                response.Films.AddRange(films);
            }
            else
                response.Films.AddRange(film);
            return response;
            
        }

        public async Task UpdateAsync()
        {
            IEnumerable<Film> films= await  _filmRepository.GetAllAsync();
            foreach (var item in films)
            {
                var url = new WebClient().DownloadString("http://www.omdbapi.com/?apikey=1bdede5a&s=" + item.title);
                var data = JsonConvert.DeserializeObject<dynamic>(url);                
                int counter = data.Search.Count;
                for (int i = 0; i < counter; i++)
                {
                    Film f = new Film()
                    {
                        imdbID = data.Search[i].imdbID.ToString(),
                        title = data.Search[i].Title.ToString(),
                        year = data.Search[i].Year.ToString(),
                        type = data.Search[i].Type.ToString(),
                        poster = data.Search[i].Poster.ToString()
                    };
                  
                    await _filmRepository.UpdateAsync(f);

                }
            }
        }
    }
}
