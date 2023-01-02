using MediatR;
using ScriptShoesAPI.Models.Shoes;

namespace ScriptShoesAPI.Features.Shoes.Queries.GetAllShoes;

public record GetAllShoesQuery : IRequest<IEnumerable<GetAllShoesDto>>
{

}