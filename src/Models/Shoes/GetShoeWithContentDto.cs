﻿using ScriptShoesAPI.Database.Entities;

namespace ScriptShoesAPI.Models.Shoes;

public class GetShoeWithContentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double CurrentPrice { get; set; }
    public double? AverageRating { get; set; }
    public string Brand { get; set; }
    public int? ReviewsNum { get; set; }
    public string ShoeType { get; set; }
    
    public virtual List<ShoeSizes>? Sizes { get; set; }
    public virtual List<Images>? Images { get; set; }
    public virtual List<MainImages> MainImages { get; set; }
}