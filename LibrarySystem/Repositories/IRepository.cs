namespace LibrarySystem.Repositories;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();

    T? GetById(int id);

    bool Add(T entity);

    bool Update(T entity);

    bool Delete(T entity);
}
