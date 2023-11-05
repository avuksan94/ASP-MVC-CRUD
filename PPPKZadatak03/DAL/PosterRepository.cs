using PPPKZadatak03.Models;

namespace PPPKZadatak03.DAL
{
    public class PosterRepository : IRepository<Poster>
    {
        private readonly PppkmoviesContext? _moviesContext;
        public PosterRepository(PppkmoviesContext? moviesContext)
        {
            if (_moviesContext == null)
            {
                _moviesContext = moviesContext;
            }
        }

        public IEnumerable<Poster>? GetAll()
        {
            return _moviesContext?.Posters;
        }

        public Poster? GetById(int id)
        {
            return GetAll()?.FirstOrDefault(p => p.PosterId ==id);
        }

        public Poster? Add(Poster entity)
        {
            if (_moviesContext != null && entity != null)
            {
                _moviesContext.Posters.Add(entity);
                _moviesContext.SaveChanges();
                return entity;
            }
            return new();
        }

        public Poster? Update(Poster entity)
        {
            if (_moviesContext != null)
            {
                Poster? poster = GetById(entity.PosterId);
                if (poster != null)
                {
                    _moviesContext.Posters.Update(poster);
                    _moviesContext.SaveChanges();

                    var updatedPoster = GetById(poster.PosterId);
                    return updatedPoster;
                }
            }
            //somekind of protection if it fails in service layer
            return new Poster();
        }


        public void Delete(int id)
        {
            if (_moviesContext != null)
            {
                Poster? poster = GetById(id);
                if (poster != null)
                {
                    _moviesContext.Posters.Remove(poster);
                    _moviesContext.SaveChanges();
                }
            }
        }
    }
}
