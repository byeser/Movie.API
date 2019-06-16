using Movie.ServiceHost.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.ServiceHost.API.ServiceAdapters
{
    public interface IOmbdService
    {
        /// <summary>
        /// Get OmdbApi Film List
        /// </summary>
        /// <param name="title"> Film Title</param>
        /// <returns>IEnumerable<Film></returns>
        IEnumerable<Film> ValidateFilm(string title);
    }
}
