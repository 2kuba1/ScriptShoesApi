using MediatR;
using ScriptShoesAPI.Models.Shoes;

namespace ScriptShoesAPI.Features.Shoes.Queries.GetshoeWithContent;

public record GetShoeWithContentQuery : IRequest<GetShoeWithContentResponse>
{
    public string ShoeName { get; set; }
}