using MediatR;
using ScriptShoesCQRS.Models.Shoes;

namespace ScriptShoesCQRS.Features.Shoes.Queries.GetShoesByName;

public class GetShoesByNameQuery : IRequest<IEnumerable<GetShoesByNameDto>>
{
    public string SearchPhrase { get; set; }
}