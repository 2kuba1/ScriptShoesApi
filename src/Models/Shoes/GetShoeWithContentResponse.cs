using ScriptShoesCQRS.Models.Reviews;

namespace ScriptShoesCQRS.Models.Shoes;

public class GetShoeWithContentResponse
{
    public GetShoeWithContentDto Shoes { get; set; }
    public IEnumerable<ReviewsDto> Reviews { get; set; }
}