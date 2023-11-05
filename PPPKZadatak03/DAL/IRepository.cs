namespace PPPKZadatak03.DAL
{
    interface IRepository<T>
    {
        IEnumerable<T>? GetAll();
        T? Add(T entity);
        T? GetById(int id);
        T? Update(T entity);
        void Delete(int id);
    }
}
