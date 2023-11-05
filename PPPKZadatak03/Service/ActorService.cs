using PPPKZadatak03.DAL;
using PPPKZadatak03.Models;

namespace PPPKZadatak03.Service
{
    public class ActorService
    {
        private readonly UnitOfWork? _unitOfWork;
        private PppkmoviesContext _pppkmoviesContext;

        public ActorService(PppkmoviesContext moviesContext)
        {
            _pppkmoviesContext = moviesContext;
            if (_unitOfWork == null)
            {
                _unitOfWork = new UnitOfWork(_pppkmoviesContext);
            }
            
        }

        public IEnumerable<Actor>? GetAll()
        {
            if (_unitOfWork != null)
            {
                return _unitOfWork?.ActorRepository?.GetAll();
            }
            return Enumerable.Empty<Actor>();
        }

        public Actor? GetById(int id)
        {
            if (_unitOfWork != null)
            {
                return _unitOfWork?.ActorRepository?.GetById(id);
            }
            return new();
        }

        public Actor? Add(Actor entity)
        {
            if (_unitOfWork != null && entity != null)
            {
                _unitOfWork?.ActorRepository?.Add(entity);
                return entity;
            }
            return new();
        }

        public Actor? Update(Actor entity)
        {
            if (_unitOfWork != null && entity != null)
            {
                Actor? actor = GetById(entity.ActorId);
                if (actor != null)
                {
                    _unitOfWork.ActorRepository?.Update(actor);

                    var updatedActor = GetById(actor.ActorId);
                    return updatedActor;
                }
            }
            return new ();
        }

        public void Delete(int id)
        {
            if (_unitOfWork != null)
            {
                Actor? actor = GetById(id);
                if (actor != null)
                {
                    _unitOfWork?.ActorRepository?.Delete(actor.ActorId);
                }
            }
        }
    }
}
