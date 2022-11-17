using MediatR;

namespace ScriptShoesCQRS.Features.Reviews.Commands.RemoveLike;

public class RemoveLikeCommand : IRequest
{
    public int ShoeId { get; set; }
    public int ReviewId { get; set; }
}