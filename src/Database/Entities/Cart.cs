namespace ScriptShoesAPI.Database.Entities;

public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ShoesId { get; set; }
}