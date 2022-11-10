using ScriptShoesApi.Entities;

namespace ScriptShoesCQRS.Database.Entities;

public class Shoes
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float? PreviousPrice { get; set; }
    public double CurrentPrice { get; set; }
    public double? AverageRating { get; set; } = 0;
    public int? CreatedBy { get; set; }
    public string Brand { get; set; }
    public int? ReviewsNum { get; set; } = 0;
    public string ShoeType { get; set; }
    public string SizesList { get; set; }

    public virtual List<Favorites> Favorites { get; set; }
    public virtual List<ShoeSizes>? Sizes { get; set; }
    public virtual List<Reviews>? Reviews { get; set; }
    public virtual List<Images>? Images { get; set; }
    public virtual List<MainImages> MainImages { get; set; }
}