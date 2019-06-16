using Movie.ServiceHost.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.ServiceHost.API.Contracts
{
    public class FilmResponse
    {
        public FilmResponse()
        {
            Films = new List<Film>();
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
        public List<Film> Films { get; set; }
    }
}
