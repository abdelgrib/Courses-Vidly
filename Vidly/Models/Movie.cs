using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        //prop + Tab
        public int Id { get; set; }

        [Required]
        public string  Name { get; set; }

        public Genre Genre { get; set; }

        [Display(Name = "Genre")]
        public byte GenreId { get; set; } // implicitement requiered

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; } // par défaut ne sont pas nullable

        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } // pour contourner le problème DateTime2 to 

        [Display(Name = "Number in Stock")]
        [Required]
        [Range(1, 20)]
        public int NumberInStock { get; set; }

        public byte NumberAvailable { get; set; }
    }
}