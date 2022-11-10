using MediatR;
using ScriptShoesCQRS.Models.Shoes;

namespace ScriptShoesCQRS.Features.Shoes.Queries.GetAllShoes;

public record GetAllShoesQuery : IRequest<IEnumerable<GetAllShoesDto>>
{

}