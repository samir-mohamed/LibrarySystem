using LibrarySystem.Dtos;

namespace LibrarySystem.Services;

public interface ICategoryService
{
    (int statusCode, string message) ValidateBook(CategoryDto categoryDto);

    CategoryToReturnDto Add(CategoryDto categoryDto);
    CategoryToReturnDto Update(int id, CategoryDto categoryDto);
    List<CategoryToReturnDto> GetAll();
    CategoryToReturnDto Get(int id);
    CategoryToReturnDto Delete(int id);
    bool Exists(int id);

    bool CategoryContaisBooks(int id);
}
