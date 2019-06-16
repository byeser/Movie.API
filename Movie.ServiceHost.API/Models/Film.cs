using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.ServiceHost.API.Models
{
    public class Film
    {
        /// <summary>
        /// Film ID 
        /// </summary>
        public string imdbID { get; set; } = string.Empty;
        /// <summary>
        /// Film Title
        /// </summary>
        public string title { get; set; } = string.Empty;
        /// <summary>
        /// Film Year
        /// </summary>
        public string year { get; set; } = string.Empty;
        /// <summary>
        /// Film Type
        /// </summary>
        public string type { get; set; } = string.Empty;
        /// <summary>
        /// Film Poster
        /// </summary>
        public string poster { get; set; } = string.Empty;
    }
}
