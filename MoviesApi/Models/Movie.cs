using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Title { get; set; }
        [Range(0.0, 10.0)]
        public double Rating { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        public virtual ICollection<MovieActor> Actors { get; set; }
        public virtual ICollection<Award> Awards { get; set; }
    }
}
