﻿using LibrarySystem.Models;

namespace LibrarySystem.Dtos;

public class CategoryToReturnDto
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}
