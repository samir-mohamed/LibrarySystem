namespace LibrarySystem.Models;

public class Book
{
    public int Bookid { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public double Price { get; set; }

    public string Author { get; set; } = string.Empty;

    public int Stock { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; } = new();
}
