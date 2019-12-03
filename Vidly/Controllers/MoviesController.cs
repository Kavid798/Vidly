using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie()
            {
                Name = "Shrek",

            };
            return View(movie);
        }
        public ActionResult Edit(int id)
        {
            return Content("id=" + id);
        }
        //movies
        //public ActionResult Index(int? pageIndex,string sortBy)
        //{
        //    if (pageIndex.HasValue)
        //        pageIndex = 1;
        //    if (string.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";
        //    return Content.Format()
        //}
        //[Route("movies/released/{year}/{month:rregex(\\d{4}):range(1`,2)"]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
        public List<Movie> GetMovies()
        {
            List<Movie> mvc = new List<Movie>
            {
                new Movie{Id=1,Name="Kappaan"},
                new Movie{Id=2,Name="Castle"},
                new Movie{Id=3,Name="Sheldon"}
            };
            return mvc;
        }
        public ActionResult DisplayMovie()
        {
            //var movie = _context.Movies.Include(c => c.Genre).ToList();
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");
            
                return View("ReadOnlyList");
        }
        public ActionResult Content(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).FirstOrDefault(k => k.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }
        [Authorize(Roles= RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieViewModel
            {
                Movie = new Movie(),
                Genres = genres
            };
            return View("New",viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                var genres = _context.Genres.ToList();
                var viewModel = new MovieViewModel
                {
                   
                    Genres = genres
                };
                return View("New", viewModel);
            }
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var mov = _context.Movies.FirstOrDefault(c => c.Id == movie.Id);
                mov.Name = movie.Name;
                mov.ReleaseDate = movie.ReleaseDate;
                mov.Genre = movie.Genre;
                mov.AvailableStocks = movie.AvailableStocks;

            }
            _context.SaveChanges();
            return RedirectToAction("New", "Movies");
        }

        public ActionResult EditMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();
            var viewModel = new MovieViewModel
            {
                Movie = movie,

                Genres = _context.Genres.ToList()
            };
            return View("New", viewModel);
        }

    }
}