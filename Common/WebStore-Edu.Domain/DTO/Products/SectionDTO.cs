﻿namespace WebStore_Edu.Domain.DTO.Products;

public class SectionDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Order { get; set; }

    public int? ParentId { get; set; }
}