namespace ScriptShoesCQRS.Models.Reviews;

public class ReviewsDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Review { get; set; }
    public int Rate { get; set; }
    public int ReviewLikes { get; set; }
    public string Title { get; set; }
    public int ShoesId { get; set; }
    public string ProfilePicture { get; set; }
}