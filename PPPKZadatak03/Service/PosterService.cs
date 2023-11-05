using PPPKZadatak03.DAL;
using PPPKZadatak03.Models;

namespace PPPKZadatak03.Service
{
    public class PosterService
    {
        private readonly UnitOfWork? _unitOfWork;

        private PppkmoviesContext _pppkmoviesContext;

        public PosterService(PppkmoviesContext moviesContext)
        {
            _pppkmoviesContext = moviesContext;
            if (_unitOfWork == null)
            {
                _unitOfWork = new UnitOfWork(_pppkmoviesContext);
            }
           
        }

        public IEnumerable<Poster>? GetAll()
        {
            if (_unitOfWork != null)
            {
                return _unitOfWork?.PosterRepository?.GetAll();
            }
            return Enumerable.Empty<Poster>();
        }

        public Poster? GetById(int id)
        {
            if (_unitOfWork != null)
            {
                return _unitOfWork?.PosterRepository?.GetById(id);
            }
            return new();
        }

        public Poster? Add(Poster entity)
        {
            if (_unitOfWork != null && entity != null)
            {
                _unitOfWork?.PosterRepository?.Add(entity);
                return entity;
            }
            return new();
        }


        public Poster? Update(Poster entity)
        {
            if (_unitOfWork != null)
            {
                Poster? poster = GetById(entity.PosterId);
                if (poster != null)
                {
                    _unitOfWork.PosterRepository?.Update(poster);

                    var updatedPoster = GetById(poster.PosterId);
                    return updatedPoster;
                }
            }
            return new();
        }

        public void Delete(int id)
        {
            if (_unitOfWork != null)
            {
                Poster? poster = GetById(id);
                if (poster != null)
                {
                    _unitOfWork?.PosterRepository?.Delete(poster.PosterId);
                }
            }
        }
    }
}
