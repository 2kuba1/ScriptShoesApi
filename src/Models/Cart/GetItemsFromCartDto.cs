using ScriptShoesAPI.Database.Entities;

namespace ScriptShoesAPI.Models.Cart;

public class GetItemsFromCartDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public double CurrentPrice { get; set; }
    public string ShoeType { get; set; }

    public virtual List<MainImages> MainImages { get; set; }
}