using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.ServiceHost.API.Models
{
    public class Film
    {
        public string imdbID { get; set; }
        public string title { get; set; }
        public string year { get; set; }
        public string type { get; set; }
        public string poster { get; set; }
    }
}
