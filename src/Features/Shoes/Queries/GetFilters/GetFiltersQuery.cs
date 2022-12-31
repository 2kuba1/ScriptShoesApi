using MediatR;
using ScriptShoesCQRS.Models.Shoes;

namespace ScriptShoesCQRS.Features.Shoes.Queries.GetFilters;

public record GetFiltersQuery : IRequest<GetFiltersDto>
{
    
}