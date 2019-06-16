using Movie.ServiceHost.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.ServiceHost.API.Contracts
{
    /// <summary>
    /// FilmResponse class
    /// </summary>
    public class FilmResponse
    {
        public FilmResponse()
        {
            Films = new List<Film>();
        }
        /// <summary>
        /// FilmResponse in message
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
        /// <summary>
        /// FilmResponse List<Film>
        /// </summary>
        public List<Film> Films { get; set; }
    }
}
