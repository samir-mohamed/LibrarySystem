using LibrarySystem.Dtos;
using LibrarySystem.HelperExtensions;
using LibrarySystem.Models;
using LibrarySystem.Repositories;

namespace LibrarySystem.Services;

public class BookService : IBookService
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<Category> _categoryRepository;

    public BookService(IRepository<Book> bookRepository, IRepository<Category> categoryRepository)
    {
        _bookRepository = bookRepository;
        _categoryRepository = categoryRepository;
    }

    public BookToReturnDto Add(BookDto bookDto)
    {
        var book = bookDto.Copy<Book>();
        var category = _categoryRepository.GetById(bookDto.CategoryId);
        book.Category = category!;
        _bookRepository.Add(book);

        var bookToReturn= book.Copy<BookToReturnDto>();
        bookToReturn.CategoryName = _categoryRepository.GetById(bookDto.CategoryId)?.Name;
        return bookToReturn;
    }

    public bool Exists(int id)
    {
        var book =_bookRepository.GetById(id);
        return book != null;
    }

    public BookToReturnDto Update(int id, BookDto bookDto)
    {
        var book = _bookRepository.GetById(id);
        book.Name = bookDto.Name;
        book.Description = bookDto.Description;
        book.Author = bookDto.Author;
        book.Price = bookDto.Price;
        book.Stock = bookDto.Stock;
        book.CategoryId = bookDto.CategoryId;
        book.Category = _categoryRepository.GetById(bookDto.CategoryId);

        _bookRepository.Update(book);
        var bookToReturn = book.Copy<BookToReturnDto>();
        bookToReturn.CategoryName = _categoryRepository.GetById(bookDto.CategoryId)?.Name;
        return bookToReturn;
    }

    public List<BookToReturnDto> GetAll()
    {
        var books = _bookRepository.GetAll();
        var dtos = new List<BookToReturnDto>();
        books.ToList().ForEach(books => dtos.Add(books.Copy<BookToReturnDto>()));
        dtos.ForEach(dto => dto.CategoryName = _categoryRepository.GetById(dto.CategoryId)?.Name);
        return dtos;
    }

    public BookToReturnDto Get(int id)
    {
        var book = _bookRepository.GetById(id);
        var bookToReturn = book!.Copy<BookToReturnDto>();
        bookToReturn.CategoryName = _categoryRepository.GetById(book.CategoryId)?.Name;
        return bookToReturn;
    }

    public BookToReturnDto Delete(int id)
    {
        var book = _bookRepository.GetById(id);
        _bookRepository.Delete(book!);
        return book.Copy<BookToReturnDto>();
    }
    public (int statusCode, string message) ValidateBook(BookDto bookDto)
    {
        if (string.IsNullOrWhiteSpace(bookDto.Name))
            return (400, "Name is Required");

        if (string.IsNullOrWhiteSpace(bookDto.Description))
            return (400, "Description is Required");

        if (bookDto.Price <= 0)
            return (400, "Price must be bigger than zero");

        if (bookDto.Stock <= 0)
            return (400, "Stock must be bigger than zero");

        var category = _categoryRepository.GetById(bookDto.CategoryId);
        if (category is null)
            return (400, $"the category {bookDto.CategoryId} is not found");

        return (200, "");
    }
}
