using LibrarySystem.Dtos;
using LibrarySystem.HelperExtensions;
using LibrarySystem.Models;
using LibrarySystem.Repositories;

namespace LibrarySystem.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public CategoryToReturnDto Add(CategoryDto categoryDto)
    {
        var category = categoryDto.Copy<Category>();
        _categoryRepository.Add(category);

        return category.Copy<CategoryToReturnDto>();
    }

    public CategoryToReturnDto Delete(int id)
    {
        var category = _categoryRepository.GetById(id);
        _categoryRepository.Delete(category!);
        return category.Copy<CategoryToReturnDto>();
    }

    public bool Exists(int id)
    {
        var category = _categoryRepository.GetById(id);
        return category != null;
    }

    public bool CategoryContaisBooks(int id)
    {
        var category = _categoryRepository.GetAllWithBooks().FirstOrDefault(c => c.CategoryId == id);
        return category?.Books?.Count > 0;
    }

    public CategoryToReturnDto Get(int id)
    {
        var category = _categoryRepository.GetById(id);
        return category!.Copy<CategoryToReturnDto>();
    }

    public List<CategoryToReturnDto> GetAll()
    {
        var categories = _categoryRepository.GetAllWithBooks();
        var dtos = new List<CategoryToReturnDto>();
        categories.ToList().ForEach(category => dtos.Add(category.Copy<CategoryToReturnDto>()));
        return dtos;
    }

    public CategoryToReturnDto Update(int id, CategoryDto categoryDto)
    {
        var category = _categoryRepository.GetById(id);
        category.Name = categoryDto.Name;
        category.Description = categoryDto.Description;
        _categoryRepository.Update(category);
        return category.Copy<CategoryToReturnDto>();
    }

    public (int statusCode, string message) ValidateBook(CategoryDto categoryDto)
    {
        if (string.IsNullOrWhiteSpace(categoryDto.Name))
            return (400, "Name is Required");

        if (string.IsNullOrWhiteSpace(categoryDto.Description))
            return (400, "Description is Required");

        return (200, "");
    }
}
