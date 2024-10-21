using LibrarySystem.Models;

namespace LibrarySystem.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    IEnumerable<Category> GetAllWithBooks();
}
