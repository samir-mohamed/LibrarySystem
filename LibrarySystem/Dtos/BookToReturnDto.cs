namespace LibrarySystem.Dtos;

public class BookToReturnDto
{
    public int Bookid { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public double Price { get; set; }

    public string Author { get; set; } = string.Empty;

    public int Stock { get; set; }

    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }
}
