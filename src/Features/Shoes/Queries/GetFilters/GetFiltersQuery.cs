using MediatR;
using ScriptShoesAPI.Models.Shoes;

namespace ScriptShoesAPI.Features.Shoes.Queries.GetFilters;

public record GetFiltersQuery : IRequest<GetFiltersDto>
{
    
}