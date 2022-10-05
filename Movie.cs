using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTemplate
{
    internal class Movie
    {
        public int ID { get; set; }
        public string TITLE { get; set; }
        public List<string> GENRE { get; set; }

        public Movie()
        {
            GENRE = new List<string>();
        }
    }
}
