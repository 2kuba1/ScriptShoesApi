using MediatR;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Models.Shoes;

namespace ScriptShoesCQRS.Features.Shoes.Queries.GetFilters;

public class GetFiltersQueryHandler : IRequestHandler<GetFiltersQuery, GetFiltersDto>
{
    private readonly AppDbContext _dbContext;

    public GetFiltersQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<GetFiltersDto> Handle(GetFiltersQuery request, CancellationToken cancellationToken)
    {
        var filters = _dbContext.Shoes
            .ToList();

        var sizesList = _dbContext.ShoeSizes
            .Select(s => s.Sizes)
            .ToList();

        var brands = new List<string>();
        var types = new List<string>();
        var sizes = new List<string>();

        for (int i = 0; i < sizesList.Count; i++)
        {
            if (!sizes.Contains(sizesList[i]))
            {
                sizes.Add(sizesList[i]);
            }
        }
            
        for (int i = 0; i < filters.Count; i++)
        {

            if (!brands.Contains(filters[i].Brand))
            {
                brands.Add(filters[i].Brand);
            }
                
            if (!types.Contains(filters[i].ShoeType))
            {
                types.Add(filters[i].ShoeType);
            }
        }

        var results = new GetFiltersDto()
        {
            Sizes = sizes,
            Brand = brands,
            ShoeType = types
        };

        return results;
    }
}