using PPPKZadatak03.Models;

namespace PPPKZadatak03.DAL
{
    public class DirectorRepository : IRepository<Director>
    {
        private readonly PppkmoviesContext? _moviesContext;
        private PppkmoviesContext _pppkmoviesContext;

        public DirectorRepository(PppkmoviesContext? moviesContext)
        {
            if (_moviesContext == null)
            {
                _moviesContext = moviesContext;
            }
        }

        public IEnumerable<Director>? GetAll()
        {
            return _moviesContext?.Directors;
        }

        public Director? GetById(int id)
        {
            return GetAll()?.FirstOrDefault(d => d.DirectorId == id);
        }

        public Director? Add(Director entity)
        {
            if (_moviesContext != null && entity != null)
            {
                _moviesContext.Directors.Add(entity);
                _moviesContext.SaveChanges();
                return entity;
            }
            return new();
        }

        public Director? Update(Director entity)
        {
            if (_moviesContext != null)
            {
                Director? director = GetById(entity.DirectorId);
                if (director != null)
                {
                    _moviesContext.Directors.Update(director);
                    _moviesContext.SaveChanges();

                    var updatedDirector = GetById(director.DirectorId);
                    return updatedDirector;
                }
            }
            //somekind of protection if it fails in service layer
            return new Director();
        }

        public void Delete(int id)
        {
            if (_moviesContext != null)
            {
                Director? director = GetById(id);
                if (director != null)
                {
                    _moviesContext.Directors.Remove(director);
                    _moviesContext.SaveChanges();
                }
            }
        }
    }
}
