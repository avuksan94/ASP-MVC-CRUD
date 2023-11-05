using PPPKZadatak03.DAL;
using PPPKZadatak03.Models;

namespace PPPKZadatak03.Service
{
    public class DirectorService
    {
        private readonly UnitOfWork? _unitOfWork;
        private PppkmoviesContext _pppkmoviesContext;

        public DirectorService(PppkmoviesContext moviesContext)
        {
            _pppkmoviesContext = moviesContext;
            if (_unitOfWork == null)
            {
                _unitOfWork = new UnitOfWork(_pppkmoviesContext);
            }
        }

        public IEnumerable<Director>? GetAll()
        {
            if (_unitOfWork != null)
            {
                return _unitOfWork?.DirectorRepository?.GetAll();
            }
            return Enumerable.Empty<Director>();
        }

        public Director? GetById(int id)
        {
            if (_unitOfWork != null)
            {
                return _unitOfWork?.DirectorRepository?.GetById(id);
            }
            return new();
        }

        public Director? Add(Director entity)
        {
            if (_unitOfWork != null && entity != null)
            {
                _unitOfWork?.DirectorRepository?.Add(entity);
                return entity;
            }
            return new();
        }

        public Director? Update(Director entity)
        {
            if (_unitOfWork != null && entity != null)
            {
                Director? director = GetById(entity.DirectorId);
                if (director != null)
                {
                    _unitOfWork.DirectorRepository?.Update(director);

                    var updatedDirector = GetById(director.DirectorId);
                    return updatedDirector;
                }
            }
            return new();
        }

        public void Delete(int id)
        {
            if (_unitOfWork != null)
            {
                Director? director = GetById(id);
                if (director != null)
                {
                    _unitOfWork?.DirectorRepository?.Delete(director.DirectorId);
                }
            }
        }
    }
}
