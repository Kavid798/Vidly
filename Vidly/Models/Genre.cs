using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
   
        public class Genre
        {
            public int Id { get; set; }
            public string Name { get; set; }

        public static implicit operator Genre(List<Movie> v)
        {
            throw new NotImplementedException();
        }
    }
    
}