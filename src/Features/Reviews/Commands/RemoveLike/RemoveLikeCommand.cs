using MediatR;

namespace ScriptShoesAPI.Features.Reviews.Commands.RemoveLike;

public record RemoveLikeCommand : IRequest
{
    public int ShoeId { get; set; }
    public int ReviewId { get; set; }
}