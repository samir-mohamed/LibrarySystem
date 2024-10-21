using LibrarySystem.Data;

namespace LibrarySystem.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly LibraryDbContext _context;

    public Repository(LibraryDbContext context)
    {
        _context = context;
    }

    public bool Add(T entity)
    {
        _context.Set<T>().Add(entity);
        return _context.SaveChanges() > 1;
    }

    public T? GetById(int id)
    {
        var entity = _context.Set<T>().Find(id);
        return entity;
    }

    public bool Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        return _context.SaveChanges() > 1;
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public bool Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return _context.SaveChanges() > 1;
    }
}
