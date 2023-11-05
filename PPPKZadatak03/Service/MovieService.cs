using PPPKZadatak03.DAL;
using PPPKZadatak03.Models;
using System.IO;

namespace PPPKZadatak03.Service
{
    public class MovieService
    {
        private readonly UnitOfWork? _unitOfWork;
        private PppkmoviesContext _pppkmoviesContext;
        public MovieService(PppkmoviesContext moviesContext) {
            _pppkmoviesContext = moviesContext;
            if (_unitOfWork == null)
            {
                _unitOfWork = new UnitOfWork(_pppkmoviesContext);
            }
        }

        public IEnumerable<Movie>? GetAll() {
            if (_unitOfWork != null)
            {
                return _unitOfWork?.MovieRepository?.GetAll();
            }
            return Enumerable.Empty<Movie>();
        }

        public Movie? GetById(int id) {
            if (_unitOfWork != null)
            {
                return _unitOfWork?.MovieRepository?.GetById(id);
            }
            return new();
        }

        public Movie? Add(Movie entity) { 
            if (_unitOfWork != null && entity != null)
            {
                _unitOfWork?.MovieRepository?.Add(entity);
                return entity;
            }
            return new();
        }

        public Movie? Update(Movie entity)
        {
            if (_unitOfWork != null && entity != null) 
            {
                Movie? movie = GetById(entity.MovieId);
                if (movie != null)
                {
                    _unitOfWork.MovieRepository?.Update(movie);

                    var updatedMovie = GetById(movie.MovieId);
                    return updatedMovie;
                }
            }
            return new();
        }

        public void Delete(int id)
        {
            if (_unitOfWork != null)
            {
                Movie? movie = GetById(id);
                if (movie != null)
                {
                    _unitOfWork?.MovieRepository?.Delete(movie.MovieId);
                }
            }
        }

        //BRIDGE RELATED ENTITIES Actor
        public void AddActorToMovie(int movieId, int actorId)
        {
            _unitOfWork?.MovieRepository?.AddActorToMovie(movieId,actorId);
        }

        public void RemoveActorFromMovie(int movieId, int actorId)
        {
            _unitOfWork?.MovieRepository?.RemoveActorFromMovie(movieId, actorId);
        }

        public IEnumerable<Movie> GetMoviesForActor(int actorId)
        {
           IEnumerable<Movie>? movies = _unitOfWork?.MovieRepository?.GetMoviesForActor(actorId);

            if (movies != null)
            {
                return movies;
            }

            return Enumerable.Empty<Movie>();
        }

        public IEnumerable<Actor> GetActorsForMovie(int movieId) 
        { 
            IEnumerable<Actor>? actors = _unitOfWork?.MovieRepository?.GetActorsForMovie(movieId);
            if (actors != null)
            {
                return actors;
            }

            return Enumerable.Empty<Actor>();
        }

        //VMMovie actorIds
        public void UpdateMovieActors(int movieId, IEnumerable<int> newActorIds)
        {
            var currentActorIds = GetActorsForMovie(movieId).Select(a => a.ActorId).ToList();

            foreach (var actorId in currentActorIds)
            {
                if (!newActorIds.Contains(actorId))
                {
                    RemoveActorFromMovie(movieId, actorId);
                }
            }

            foreach (var actorId in newActorIds)
            {
                if (!currentActorIds.Contains(actorId))
                {
                    AddActorToMovie(movieId, actorId);
                }
            }
        }

        //BRIDGE RELATED ENTITIES Director
        public void AddDirectorToMovie(int movieId, int directorId)
        {
            _unitOfWork?.MovieRepository?.AddDirectorToMovie(movieId,directorId);
        }

        public void RemoveDirectorFromMovie(int movieId, int directorId)
        {
            _unitOfWork?.MovieRepository?.RemoveDirectorFromMovie(movieId,directorId);
        }

        public IEnumerable<Movie> GetMoviesForDirector(int directorId)
        {
            IEnumerable<Movie>? movies = _unitOfWork?.MovieRepository?.GetMoviesForDirector(directorId);

            if (movies != null)
            {
                return movies;
            }

            return Enumerable.Empty<Movie>();
        }

        public IEnumerable<Director> GetDirectorsForMovie(int movieId)
        {
            IEnumerable<Director>? directors = _unitOfWork?.MovieRepository?.GetDirectorsForMovie(movieId);
            if (directors != null)
            {
                return directors;
            }

            return Enumerable.Empty<Director>();
        }

        //VMMovie directorIds
        public void UpdateMovieDirectors(int movieId, IEnumerable<int> newDirectorIds)
        {
            var currentDirectorIds = GetDirectorsForMovie(movieId).Select(d => d.DirectorId).ToList();

            foreach (var directorId in currentDirectorIds)
            {
                if (!newDirectorIds.Contains(directorId))
                {
                    RemoveDirectorFromMovie(movieId, directorId);
                }
            }

            foreach (var directorId in newDirectorIds)
            {
                if (!currentDirectorIds.Contains(directorId))
                {
                    AddDirectorToMovie(movieId, directorId);
                }
            }
        }

        //BRIDGE RELATED ENTITIES Movies
        public void AddPosterToMovie(int movieId, int posterId) 
        {
            _unitOfWork?.MovieRepository?.AddPosterToMovie(movieId,posterId);
        }

        public void RemovePosterFromMovie(int movieId, int posterId) 
        {
            _unitOfWork?.MovieRepository?.RemovePosterFromMovie(movieId, posterId);
        }

        public IEnumerable<Movie> GetMoviesForPoster(int posterId) 
        {
            IEnumerable<Movie>? movies = _unitOfWork?.MovieRepository?.GetMoviesForPoster(posterId);

            if (movies != null)
            {
                return movies;
            }

            return Enumerable.Empty<Movie>();
        }

        public IEnumerable<Poster> GetPostersForMovie(int movieId) 
        {
            IEnumerable<Poster>? posters = _unitOfWork?.MovieRepository?.GetPostersForMovie(movieId);
            if (posters != null)
            {
                return posters;
            }

            return Enumerable.Empty<Poster>();
        }

        //VMMovie posterIds
        public void UpdateMoviePosters(int movieId, IEnumerable<int> newPosterIds)
        {
            var currentPosterIds = GetPostersForMovie(movieId).Select(p => p.PosterId).ToList();

            foreach (var posterId in currentPosterIds)
            {
                if (!newPosterIds.Contains(posterId))
                {
                    RemovePosterFromMovie(movieId, posterId);
                }
            }

            foreach (var posterId in newPosterIds)
            {
                if (!currentPosterIds.Contains(posterId))
                {
                    AddPosterToMovie(movieId, posterId);
                }
            }
        }
    }
}
