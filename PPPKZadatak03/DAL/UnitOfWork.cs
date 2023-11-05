using PPPKZadatak03.Models;

namespace PPPKZadatak03.DAL
{
    public class UnitOfWork 
    {
        private  PppkmoviesContext? _pppkmoviesContext;
        private  MovieRepository? _movieRepository;
        private  ActorRepository? _actorRepository;
        private  DirectorRepository? _directorRepository;
        private  PosterRepository? _posterRepository;

        public UnitOfWork(PppkmoviesContext pppkmoviesContext) {
            if (_pppkmoviesContext == null)
            {
                _pppkmoviesContext = pppkmoviesContext;
            }
        }

        public MovieRepository? MovieRepository
        {
            get {
                if (_pppkmoviesContext != null)
                {
                    if (_movieRepository == null)
                    {
                        _movieRepository = new MovieRepository(_pppkmoviesContext);
                    }
                }
                return _movieRepository;
            }
        }

        public ActorRepository? ActorRepository
        {
            get
            {
                if (_pppkmoviesContext != null)
                {
                    if (_actorRepository == null)
                    {
                        _actorRepository = new ActorRepository(_pppkmoviesContext);
                    }
                }
                return _actorRepository;
            }
        }

        public DirectorRepository? DirectorRepository
        {
            get
            {
                if (_pppkmoviesContext != null)
                {
                    if (_directorRepository == null)
                    {
                        _directorRepository = new DirectorRepository(_pppkmoviesContext);
                    }
                }
                return _directorRepository;
            }
        }

        public PosterRepository? PosterRepository
        {
            get
            {
                if (_pppkmoviesContext != null)
                {
                    if (_posterRepository == null)
                    {
                        _posterRepository = new PosterRepository(_pppkmoviesContext);
                    }
                }
                return _posterRepository;
            }
        }

    }
}
