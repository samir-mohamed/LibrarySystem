using LibrarySystem.Data;
using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(LibraryDbContext context) : 
        base(context)
    {
    }

    public IEnumerable<Category> GetAllWithBooks()
    {
        var categories = _context.Categories.Include(c => c.Books).ToList();
        return categories;
    }
}
