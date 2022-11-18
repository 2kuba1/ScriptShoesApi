using ScriptShoesApi.Entities;

namespace ScriptShoesCQRS.Models.Favourites;

public class GetFavouritesDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double CurrentPrice { get; set; }
    public string ShoeType { get; set; }
    public double AverageRating { get; set; }
    public int ReviewsNum { get; set; }
    public string Brand { get; set; }
    public List<MainImages> MainImages { get; set; }
}