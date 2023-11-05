using Microsoft.EntityFrameworkCore;
using PPPKZadatak03.Models;

namespace PPPKZadatak03.DAL
{
    public class MovieRepository : IRepository<Movie>
    {
        private readonly PppkmoviesContext? _moviesContext;

        public MovieRepository(PppkmoviesContext moviesContext)
        {
            if (_moviesContext == null)
            {
                _moviesContext = moviesContext;
            }
        }

        public IEnumerable<Movie>? GetAll()
        {
            return _moviesContext?.Movies;
        }

        public Movie? GetById(int id)
        {
            return _moviesContext?.Movies?
                .Include(m => m.Posters)
                .FirstOrDefault(m => m.MovieId == id);
        }

        public Movie? Add(Movie entity)
        {
            if (_moviesContext != null && entity != null)
            {
                _moviesContext.Movies.Add(entity);
                _moviesContext.SaveChanges();
                return entity;
            }
            return new();
        }

        public Movie? Update(Movie entity)
        {
            if (_moviesContext != null)
            {
                Movie? movie = GetById(entity.MovieId);
                if (movie != null)
                {
                    _moviesContext.Movies.Update(movie);
                    _moviesContext.SaveChanges();

                    var updatedMovie = GetById(movie.MovieId);
                    return updatedMovie;
                }
            }
            //somekind of protection if it fails in service layer
            return new Movie();

        }

        public void Delete(int id)
        {
            if (_moviesContext != null)
            {
                Movie? movie = GetById(id);
                if (movie != null)
                {
                    _moviesContext.Movies.Remove(movie);
                    _moviesContext.SaveChanges();
                }
            }
        }

        //BRIDGE TABLE RELATED METHODS MovieActor

        public void AddActorToMovie(int movieId, int actorId)
        {
            var movie = _moviesContext?.Movies.Include(m => m.Actors).FirstOrDefault(m => m.MovieId == movieId);
            var actor = _moviesContext?.Actors.Find(actorId);

            if (movie != null && actor != null)
            {
                movie.Actors.Add(actor);
                _moviesContext?.SaveChanges();
            }
        }

        public void RemoveActorFromMovie(int movieId, int actorId)
        {
            var movie = _moviesContext?.Movies.Include(m => m.Actors).FirstOrDefault(m => m.MovieId == movieId);
            var actor = _moviesContext?.Actors.Find(actorId);

            if (movie != null && actor != null && movie.Actors.Contains(actor))
            {
                movie.Actors.Remove(actor);
                _moviesContext?.SaveChanges();
            }
        }

        public IEnumerable<Movie> GetMoviesForActor(int actorId)
        {
            return _moviesContext?.Actors
                                 .Include(a => a.Movies)
                                 .FirstOrDefault(a => a.ActorId == actorId)?.Movies
                                 ?? Enumerable.Empty<Movie>();
        }

        public IEnumerable<Actor> GetActorsForMovie(int movieId)
        {
            return _moviesContext?.Movies
                                 .Include(m => m.Actors)
                                 .FirstOrDefault(m => m.MovieId == movieId)?.Actors
                                 ?? Enumerable.Empty<Actor>();
        }

        //BRIDGE TABLE RELATED METHODS MovieDirector

        public void AddDirectorToMovie(int movieId, int directorId)
        {
            var movie = _moviesContext?.Movies.Include(m => m.Directors).FirstOrDefault(m => m.MovieId == movieId);
            var director = _moviesContext?.Directors.Find(directorId);

            if (movie != null && director != null)
            {
                movie.Directors.Add(director);
                _moviesContext?.SaveChanges();
            }
        }

        public void RemoveDirectorFromMovie(int movieId, int directorId)
        {
            var movie = _moviesContext?.Movies.Include(m => m.Directors).FirstOrDefault(m => m.MovieId == movieId);
            var director = _moviesContext?.Directors.Find(directorId);

            if (movie != null && director != null && movie.Directors.Contains(director))
            {
                movie.Directors.Remove(director);
                _moviesContext?.SaveChanges();
            }
        }

        public IEnumerable<Movie> GetMoviesForDirector(int directorId)
        {
            return _moviesContext?.Directors
                                 .Include(d => d.Movies)
                                 .FirstOrDefault(d => d.DirectorId == directorId)?.Movies
                                 ?? Enumerable.Empty<Movie>();
        }

        public IEnumerable<Director> GetDirectorsForMovie(int movieId)
        {
            return _moviesContext?.Movies
                                 .Include(m => m.Directors)
                                 .FirstOrDefault(m => m.MovieId == movieId)?.Directors
                                 ?? Enumerable.Empty<Director>();
        }

        //BRIDGE TABLE RELATED METHODS MoviePoster

        public void AddPosterToMovie(int movieId, int posterId)
        {
            var movie = _moviesContext?.Movies.Include(m => m.Posters).FirstOrDefault(m => m.MovieId == movieId);
            var poster = _moviesContext?.Posters.Find(posterId);

            if (movie != null && poster != null)
            {
                movie.Posters.Add(poster);
                _moviesContext?.SaveChanges();
            }
        }

        public void RemovePosterFromMovie(int movieId, int posterId)
        {
            var movie = _moviesContext?.Movies.Include(m => m.Posters).FirstOrDefault(m => m.MovieId == movieId);
            var poster = _moviesContext?.Posters.Find(posterId);

            if (movie != null && poster != null && movie.Posters.Contains(poster))
            {
                movie.Posters.Remove(poster);
                _moviesContext?.SaveChanges();
            }
        }

        public IEnumerable<Movie> GetMoviesForPoster(int posterId)
        {
            return _moviesContext?.Posters
                                 .Include(p => p.Movies)
                                 .FirstOrDefault(p => p.PosterId == posterId)?.Movies
                                 ?? Enumerable.Empty<Movie>();
        }

        public IEnumerable<Poster> GetPostersForMovie(int movieId)
        {
            return _moviesContext?.Movies
                                 .Include(m => m.Posters)
                                 .FirstOrDefault(m => m.MovieId == movieId)?.Posters
                                 ?? Enumerable.Empty<Poster>();
        }
    }
}
