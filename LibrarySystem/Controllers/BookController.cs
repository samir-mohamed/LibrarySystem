using LibrarySystem.Dtos;
using LibrarySystem.Errors;
using LibrarySystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var booksToReturn = _bookService.GetAll();
        return Ok(booksToReturn);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (!_bookService.Exists(id))
            return NotFound(new ApiResponse(404, $"the book with id = {id} is not found"));

        var bookToReturn = _bookService.Get(id);

        return Ok(bookToReturn);
    }

    [HttpPost]
    public IActionResult Add([FromBody] BookDto bookDto)
    {
        var (statusCode, message) = _bookService.ValidateBook(bookDto);
        if(statusCode != 200)
            return BadRequest(new ApiResponse(statusCode, message));

        var bookToReturnDto = _bookService.Add(bookDto);

        return CreatedAtAction(nameof(Get), new { id = bookToReturnDto.Bookid }, bookToReturnDto);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] BookDto bookDto)
    {
        if(!_bookService.Exists(id))
            return NotFound(new ApiResponse(404, $"the book with id = {id} is not found"));

        var (statusCode, message) = _bookService.ValidateBook(bookDto);
        if (statusCode != 200)
            return BadRequest(new ApiResponse(statusCode, message));

        var bookToReturn = _bookService.Update(id, bookDto);
        return Ok(bookToReturn);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_bookService.Exists(id))
            return NotFound(new ApiResponse(404, $"the book with id = {id} is not found"));

        _bookService.Delete(id);
        return Ok();
    }
}
