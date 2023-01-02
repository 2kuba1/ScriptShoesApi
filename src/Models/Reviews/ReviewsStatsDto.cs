namespace ScriptShoesAPI.Models.Reviews;

public class ReviewsStatsDto
{
    public int OneStarCount { get; set; }
    public int TwoStarCount { get; set; }
    public int ThreeStarCount { get; set; }
    public int FourStarCount { get; set; }
    public int FiveStarCount { get; set; }

    public double AvgRate { get; set; }
}