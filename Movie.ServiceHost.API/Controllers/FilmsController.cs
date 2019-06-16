using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Movie.ServiceHost.API.Business;
using Movie.ServiceHost.API.Contracts;
using Movie.ServiceHost.API.Models;

namespace Movie.ServiceHost.API.Controllers
{
    [EnableCors("CorsPolicy")]
    public class FilmsController : Controller
    {
        private readonly IFilmBusiness _filmBusiness;
        private ILogger<FilmsController> _logger;
        private readonly IMemoryCache _memoryCache;
        public FilmsController(IFilmBusiness filmBusiness, ILogger<FilmsController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _filmBusiness = filmBusiness;
            _memoryCache = memoryCache;
        }
        // GET api/films/       
        [Route("api/Films/GetAll")]
        
        public async Task<FilmResponse> GetAll()
        {
            return await _filmBusiness.GetAllAsync();
        }

        // GET api/films/{title}
        [Route("api/Films/Get/{title}")]
        public async Task<FilmResponse> Get(string title)
        {
            return await _filmBusiness.GetAsync(title);
        }


        // POST api/films
        [ProducesResponseType(201)]
        [HttpPost]
        public async Task Post([FromBody]Film film)
        {
            await _filmBusiness.AddAsync(film);
        }
        [HttpGet]
        [Route("api/cachemanagement/clear")]
        public Boolean ClearCache()
        {
            _memoryCache.Remove("filmCacheSearch");
            return true;
        }

    }
}