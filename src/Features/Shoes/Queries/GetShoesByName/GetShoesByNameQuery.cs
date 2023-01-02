using MediatR;
using ScriptShoesAPI.Models.Shoes;

namespace ScriptShoesAPI.Features.Shoes.Queries.GetShoesByName;

public record GetShoesByNameQuery : IRequest<IEnumerable<GetShoesByNameDto>>
{
    public string SearchPhrase { get; set; }
}