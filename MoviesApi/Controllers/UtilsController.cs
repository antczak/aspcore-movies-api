using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;

namespace MoviesApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Utils")]
    public class UtilsController : Controller
    {
        private readonly MoviesApiContext _context;

        public UtilsController(MoviesApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Fill")]
        public async Task<IActionResult> FillWithData()
        {
            var movies = new[]
            {
                new Movie() { Title = "Avengers: Infinity War", Rating = 8.3 , ReleaseDate = new DateTime(2018, 4, 23) },
                new Movie() { Title = "Deadpool 2", Rating = 7.8, ReleaseDate = new DateTime(2018, 5, 10) },
                new Movie() { Title = "Spider-Man: Homecoming", Rating = 8.3, ReleaseDate = new DateTime(2017, 6, 28) }
            }.ToList();

            var actors = new[]
            {
                new Actor() { FirstName = "Robert", LastName = "Downey", BirthDate = new DateTime(1965, 4, 4) },
                new Actor() { FirstName = "Tom", LastName = "Holland", BirthDate = new DateTime(1996, 6, 1) },
                new Actor() { FirstName = "Ryan", LastName = "Reynolds", BirthDate = new DateTime(1976, 10, 23) },
            }.ToList();

            var awards = new[]
            {
                new Award(){ Name = "BAFTA Rising Star"}
            }.ToList();

            foreach (var movie in movies)
            {
                movie.Actors = new List<MovieActor>();
            }

            foreach (var actor in actors)
            {
                actor.Awards = new List<Award>();
            }

            movies[0].Actors.Add(new MovieActor() { Actor = actors[0], Movie = movies[0] });
            movies[0].Actors.Add(new MovieActor() { Actor = actors[1], Movie = movies[0] });
            movies[1].Actors.Add(new MovieActor() { Actor = actors[2], Movie = movies[1] });
            movies[2].Actors.Add(new MovieActor() { Actor = actors[0], Movie = movies[2] });
            movies[2].Actors.Add(new MovieActor() { Actor = actors[1], Movie = movies[2] });

            actors[1].Awards.Add(awards[0]);

            _context.Movie.AddRange(movies);
            _context.Actors.AddRange(actors);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}