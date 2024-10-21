﻿namespace LibrarySystem.Models;

public class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public List<Book> Books { get; set; } = new();
}
