using PPPKZadatak03.Models;

namespace PPPKZadatak03.DAL
{
    public class ActorRepository : IRepository<Actor>
    {
        private readonly PppkmoviesContext? _moviesContext;
        public ActorRepository(PppkmoviesContext? moviesContext)
        {
            if (_moviesContext == null)
            {
                _moviesContext = moviesContext;
            }
        }

        public IEnumerable<Actor>? GetAll()
        {
            return _moviesContext?.Actors;
        }

        public Actor? GetById(int id)
        {
            return GetAll()?.FirstOrDefault(a => a.ActorId == id);
        }

        public Actor? Add(Actor entity)
        {
            if (_moviesContext != null && entity != null)
            {
                _moviesContext.Actors.Add(entity);
                _moviesContext.SaveChanges();
                return entity;
            }
            return new();
        }


        public Actor? Update(Actor entity)
        {
            if (_moviesContext != null)
            {
                Actor? actor = GetById(entity.ActorId);
                if (actor != null)
                {
                    _moviesContext.Actors.Update(actor);
                    _moviesContext.SaveChanges();

                    var updatedActor = GetById(actor.ActorId);
                    return updatedActor;
                }
            }
            //somekind of protection if it fails in service layer
            return new Actor();
        }

        public void Delete(int id)
        {
            if (_moviesContext != null)
            {
                Actor? actor = GetById(id);
                if (actor != null)
                {
                    _moviesContext.Actors.Remove(actor);
                    _moviesContext.SaveChanges();
                }
            }
        }
    }
}
