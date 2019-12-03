using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Vidly.Models
{
    public class Movie
    {
        
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [Display(Name = "Movie Name")]
        [StringLength(50)]
        public string Name { get; set; }
        
        public Genre Genre { get; set; }
        [Required]
        public int? GenreId { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        
        public DateTime ReleaseDate { get; set; }
        
        public DateTime DateAdded { get; set; }

        [Display(Name = "Available Stock")]

        [Required]
        [Range(1,20)]
        public int? AvailableStocks { get; set; }


    }

}