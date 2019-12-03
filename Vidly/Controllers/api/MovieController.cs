using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using Vidly.DTO;
using Vidly.Models;
using System.Data.Entity;
namespace Vidly.Controllers.api
{
    public class MovieController : ApiController
    {
        private ApplicationDbContext _context = null;
        public MovieController()
        {
            _context = new ApplicationDbContext();
        }
        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movies.Include(c=>c.Genre).ToList().Select(Mapper.Map<Movie, MovieDto>);
        }
        public IHttpActionResult GetMovies(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return NotFound();
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);

        }
        [System.Web.Http.HttpPut]
        public void UpdateMovie(int id, MovieDto movie)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var movieIndb = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movieIndb == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
        
            Mapper.Map(movie, movieIndb);
            _context.SaveChanges();
        }
        [System.Web.Http.HttpDelete]
        public void DeleteMovie(int id)
        {
            
            var movieindb = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movieindb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Movies.Remove(movieindb);
            _context.SaveChanges();

        }
    }
}
