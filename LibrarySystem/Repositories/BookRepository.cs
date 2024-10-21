using LibrarySystem.Data;

namespace LibrarySystem.Repositories;

public class BookRepository<Book> : Repository<Book> where Book : class
{
    public BookRepository(LibraryDbContext context) 
        : base(context)
    {
    }
}
