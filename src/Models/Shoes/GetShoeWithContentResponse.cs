using ScriptShoesAPI.Models.Reviews;

namespace ScriptShoesAPI.Models.Shoes;

public class GetShoeWithContentResponse
{
    public GetShoeWithContentDto Shoes { get; set; }
    public IEnumerable<ReviewsDto> Reviews { get; set; }
}