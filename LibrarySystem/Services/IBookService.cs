using LibrarySystem.Dtos;
using LibrarySystem.Models;

namespace LibrarySystem.Services;

public interface IBookService
{
    (int statusCode, string message) ValidateBook(BookDto bookDto);

    BookToReturnDto Add(BookDto bookDto);
    BookToReturnDto Update(int id, BookDto bookDto);
    List<BookToReturnDto> GetAll();
    BookToReturnDto Get(int id);
    BookToReturnDto Delete(int id);
    bool Exists(int id);
}
