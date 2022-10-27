using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ApplicationTemplate.Models
{
    public class Movie : Media
    {
        public string Genres { get; set; }

        public override string ToString()
        {
            return $"MOVIE: {Title}, {Genres}";
        }
    }
}
