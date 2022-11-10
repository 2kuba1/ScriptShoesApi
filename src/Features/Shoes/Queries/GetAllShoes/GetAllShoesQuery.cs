using MediatR;
using ScriptShoesCQRS.Models.Shoes;

namespace ScriptShoesCQRS.Features.Shoes.Queries.GetAllShoes;

public class GetAllShoesQuery : IRequest<IEnumerable<GetAllShoesDto>>
{

}