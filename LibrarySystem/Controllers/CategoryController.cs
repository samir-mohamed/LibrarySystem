using LibrarySystem.Dtos;
using LibrarySystem.Errors;
using LibrarySystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var categoriesToReturn = _categoryService.GetAll();
        return Ok(categoriesToReturn);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (!_categoryService.Exists(id))
            return NotFound(new ApiResponse(404, $"the category with id = {id} is not found"));

        var categoryToReturn = _categoryService.Get(id);

        return Ok(categoryToReturn);
    }

    [HttpPost]
    public IActionResult Add([FromBody]CategoryDto categoryDto)
    {
        var (statusCode, message) = _categoryService.ValidateBook(categoryDto);
        if (statusCode != 200)
            return BadRequest(new ApiResponse(statusCode, message));

        var categoryToReturnDto = _categoryService.Add(categoryDto);

        return CreatedAtAction(nameof(Get), new { id = categoryToReturnDto.CategoryId }, categoryToReturnDto);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody]CategoryDto categoryDto)
    {
        if (!_categoryService.Exists(id))
            return NotFound(new ApiResponse(404, $"the category with id = {id} is not found"));

        var (statusCode, message) = _categoryService.ValidateBook(categoryDto);
        if (statusCode != 200)
            return BadRequest(new ApiResponse(statusCode, message));

        var categoryToReturn = _categoryService.Update(id, categoryDto);
        return Ok(categoryToReturn);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_categoryService.Exists(id))
            return NotFound(new ApiResponse(404, $"the category with id = {id} is not found"));

        if(_categoryService.CategoryContaisBooks(id))
            return BadRequest(new ApiResponse(400, $"the category with id = {id} contains books, you have to delete it first"));

        _categoryService.Delete(id);
        return Ok();
    }
}
