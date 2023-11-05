using Microsoft.AspNetCore;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PPPKZadatak03.Models;
using PPPKZadatak03.Service;
using PPPKZadatak03.ViewModels;

namespace PPPKZadatak03.Controllers
{
    public class MovieController : Controller
    {
        private readonly ILogger<MovieController>? _logger;
        private readonly MovieService? _movieService;
        private readonly ActorService? _actorService;
        private readonly DirectorService? _directorService;
        private readonly PosterService? _posterService;

        public MovieController(ILogger<MovieController>? logger, MovieService movieService, ActorService? actorService, DirectorService? directorService,PosterService posterService)
        {
            _logger = logger;
            _movieService = movieService;
            _actorService = actorService;
            _directorService = directorService;
            _posterService = posterService;
        }

        // GET: MovieController
        public ActionResult Index()
        {
            List<Movie>? movies = _movieService?.GetAll()?.ToList();
            List<VMMovie>? vMovies = new();
            if (movies != null)
            {
                foreach (Movie? movie in movies)
                {
                    IEnumerable<Actor>? actors = _movieService?.GetActorsForMovie(movie.MovieId);
                    IEnumerable<Director>? directors = _movieService?.GetDirectorsForMovie(movie.MovieId);
                    IEnumerable<Poster>? posters = _movieService?.GetPostersForMovie(movie.MovieId);

                    List<string>? actorsForMovie = new();
                    List<string>? directorsForMovie = new();
                    List<string>? postersForMovie = new();

                    actors?.ToList().ForEach(a => actorsForMovie.Add(a.Name));
                    directors?.ToList().ForEach(d => directorsForMovie.Add(d.Name));
                    posters?.ToList().ForEach(p => postersForMovie.Add(p.Content));

                    VMMovie? vMovie = new VMMovie() {
                        MovieId = movie.MovieId,
                        Title = movie.Title,
                        ReleaseDate = movie.ReleaseDate,
                        Genre = movie.Genre,
                        ActorNames = actorsForMovie,
                        DirectorNames = directorsForMovie,
                        PosterLinks = postersForMovie                       
                    };

                    
                    vMovies.Add(vMovie);
                }
            }
           

            return View(vMovies);
        }

        // GET: MovieController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MovieController/Create
        public ActionResult Create()
        {
            ViewBag.Actors = _actorService?.GetAll()?.Select(a => new SelectListItem
            {
                Value = a.ActorId.ToString(),
                Text = a.Name
            }).ToList();

            ViewBag.Directors = _directorService?.GetAll()?.Select(d => new SelectListItem
            {
                Value = d.DirectorId.ToString(),
                Text = d.Name
            }).ToList();

            ViewBag.Posters = _posterService?.GetAll()?.Select(p => new SelectListItem
            {
                Value = p.PosterId.ToString(),
                Text = p.Content
            }).ToList();

            return View();
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMMovie movieViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(movieViewModel);
                }

                // Map ViewModel to Domain Model
                Movie newMovie = new Movie
                {
                    Title = movieViewModel.Title,
                    Genre = movieViewModel.Genre,
                    ReleaseDate = movieViewModel.ReleaseDate
                };

                _logger?.LogInformation("Actor count" + movieViewModel.SelectedActorIds.Count().ToString());

                Movie? newAddedMovie =_movieService?.Add(newMovie);

                if (newAddedMovie != null)
                {
                    foreach (var id in movieViewModel.SelectedActorIds)
                    {
                        Actor? actor = _actorService?.GetById(id);
                        _movieService?.AddActorToMovie(newAddedMovie.MovieId, id);
                    }

                    foreach (var id in movieViewModel.SelectedDirectorIds)
                    {
                        Director? director = _directorService?.GetById(id);
                        _movieService?.AddDirectorToMovie(newAddedMovie.MovieId, id);
                    }

                    foreach (var id in movieViewModel.SelectedPosterLinks)
                    {
                        Poster? Poster = _posterService?.GetById(id);
                        _movieService?.AddPosterToMovie(newAddedMovie.MovieId, id);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // If something goes wrong, reload the view with the data the user had entered
                return View(movieViewModel);
            }
        }


        // GET: MovieController/Edit/5
        public ActionResult Edit(int id)
        {
            // Fetch the movie by its ID
            Movie? movie = _movieService?.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }

            var allActors = _actorService?.GetAll() ?? Enumerable.Empty<Actor>();
            var allDirectors = _directorService?.GetAll() ?? Enumerable.Empty<Director>();
            var allPosters = _posterService?.GetAll() ?? Enumerable.Empty<Poster>();

            var actorsForMovie = _movieService?.GetActorsForMovie(id) ?? Enumerable.Empty<Actor>();
            var directorsForMovie = _movieService?.GetDirectorsForMovie(id) ?? Enumerable.Empty<Director>();
            var postersForMovie = _movieService?.GetPostersForMovie(id) ?? Enumerable.Empty<Poster>();

            // Exclude actors, directors, and posters that are already associated with the movie
            var availableActors = allActors.Except(actorsForMovie);
            var availableDirectors = allDirectors.Except(directorsForMovie);
            var availablePosters = allPosters;

            // Map the movie to the VMMovie view model
            VMMovie vmMovie = new VMMovie
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Genre = movie.Genre,
                ActorNames = movie.Actors.Select(a => a.Name).ToList(),
                DirectorNames = movie.Directors.Select(d => d.Name).ToList(),
                PosterLinks = movie.Posters.Select(p => p.Content).ToList(),
                AvailableActors = availableActors.ToList(),
                AvailableDirectors = availableDirectors.ToList(),
                AvailablePosters = availablePosters.ToList()
            };

            // Set the ViewBag properties for actors, directors, and posters
            ViewBag.AvailableActors = availableActors.Select(a => new SelectListItem
            {
                Value = a.ActorId.ToString(),
                Text = a.Name
            }).ToList();

            ViewBag.AvailableDirectors = availableDirectors.Select(d => new SelectListItem
            {
                Value = d.DirectorId.ToString(),
                Text = d.Name
            }).ToList();

            ViewBag.AvailablePosters = availablePosters.Select(p => new SelectListItem
            {
                Value = p.PosterId.ToString(),
                Text = p.Content
            }).ToList();

            ViewBag.SelectedActors = actorsForMovie?.Select(a => new SelectListItem
            {
                Value = a.ActorId.ToString(),
                Text = a.Name
            }).ToList();

            ViewBag.SelectedDirectors = directorsForMovie?.Select(d => new SelectListItem
            {
                Value = d.DirectorId.ToString(),
                Text = d.Name
            }).ToList();

            ViewBag.SelectedPosters = postersForMovie?.Select(p => new SelectListItem
            {
                Value = p.PosterId.ToString(),
                Text = p.Content
            }).ToList();

            return View(vmMovie);
        }



        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VMMovie movieViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(movieViewModel);
                }


                Movie? movie = _movieService?.GetById(id);

                movie.Title = movieViewModel.Title;
                movie.Genre = movieViewModel.Genre;
                movie.ReleaseDate = movieViewModel.ReleaseDate;

                _movieService?.Update(movie);

                // Handle the associations with actors, directors, and posters.
                _movieService?.UpdateMovieActors(id, movieViewModel.SelectedActorIds);
                _movieService?.UpdateMovieDirectors(id, movieViewModel.SelectedDirectorIds);
                _movieService?.UpdateMoviePosters(id, movieViewModel.SelectedPosterLinks);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.AvailableActors = _actorService?.GetAll();
                ViewBag.SelectedActors = _movieService?.GetActorsForMovie(id);

                ViewBag.AvailableDirectors = _directorService?.GetAll();
                ViewBag.SelectedDirectors = _movieService?.GetDirectorsForMovie(id);

                ViewBag.AvailablePosters = _posterService?.GetAll();
                ViewBag.SelectedPosters = _movieService?.GetPostersForMovie(id);

                return View(movieViewModel);
            }
        }

        // GET: MovieController/Delete/5
        public ActionResult Delete(int id)
        {
            var movie = _movieService?.GetById(id);
            return View(movie);
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Movie? movie = _movieService?.GetById(id);
                var actorsForMovie = _movieService?.GetActorsForMovie(id);
                var directorsForMovie = _movieService?.GetDirectorsForMovie(id);
                var postersForMovie = _movieService?.GetPostersForMovie(id);

                if (movie !=  null)
                {
                    if (actorsForMovie != null)
                    {
                        var actorIdsToRemove = actorsForMovie.Select(a => a.ActorId).ToList();
                        foreach (var actorId in actorIdsToRemove)
                        {
                            _movieService?.RemoveActorFromMovie(id, actorId);
                        }
                    }

                    if (directorsForMovie != null)
                    {
                        var directorIdsToRemove = directorsForMovie.Select(d => d.DirectorId).ToList();
                        foreach (var directorId in directorIdsToRemove)
                        {
                            _movieService?.RemoveDirectorFromMovie(id, directorId);
                        }
                    }

                    if (postersForMovie != null)
                    {
                        var posterIdsToRemove = movie.Posters.Select(p => p.PosterId).ToList();
                        foreach (var posterId in posterIdsToRemove)
                        {
                            _movieService?.RemovePosterFromMovie(id, posterId);
                        }
                    }
                }

                _movieService?.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
