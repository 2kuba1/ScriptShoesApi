namespace ScriptShoesCQRS.Database.Entities;

public class ReviewsLikes
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ShoesId { get; set; }
    public int ReviewId { get; set; }
}