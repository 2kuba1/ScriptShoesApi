namespace ScriptShoesAPI.Models.Reviews;

public class CreateReviewDto
{
    public string Title { get; set; }
    public string Review { get; set; }
    public int Rate { get; set; }
}