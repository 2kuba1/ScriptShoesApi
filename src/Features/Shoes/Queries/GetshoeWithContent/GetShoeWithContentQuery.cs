using MediatR;
using ScriptShoesCQRS.Models.Shoes;

namespace ScriptShoesCQRS.Features.Shoes.Queries.GetShoeWithContent;

public record GetShoeWithContentQuery : IRequest<GetShoeWithContentResponse>
{
    public string ShoeName { get; set; }
}