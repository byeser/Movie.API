using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Movie.ServiceHost.API.Models;
using Newtonsoft.Json;

namespace Movie.ServiceHost.API.ServiceAdapters
{
    public class OmdbServiceAdapter : IOmbdService
    {
        /// <summary>
        /// Get OmdbApi Film List
        /// </summary>
        /// <param name="title">Film Title</param>
        /// <returns>IEnumerable<Film></returns>
        public IEnumerable<Film> ValidateFilm(string title)
        {
            var url = new WebClient().DownloadString("http://www.omdbapi.com/?apikey=1bdede5a&s=" + title);
            var data = JsonConvert.DeserializeObject<dynamic>(url);
            List<Film> films = new List<Film>();
            int counter = data.Search.Count;
            for (int i = 0; i < counter; i++)
            {
                Film film = new Film()
                {
                    imdbID = data.Search[i].imdbID.ToString(),
                    title = data.Search[i].Title.ToString(),
                    year = data.Search[i].Year.ToString(),
                    type = data.Search[i].Type.ToString(),
                    poster = data.Search[i].Poster.ToString()
                };
                films.Add(film);             

            }
            return films;
        }
    }
}
