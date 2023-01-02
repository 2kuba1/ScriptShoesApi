using MediatR;
using ScriptShoesAPI.Database;
using ScriptShoesAPI.Models.Shoes;

namespace ScriptShoesAPI.Features.Shoes.Queries.GetFilters;

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

        foreach (var t in sizesList.Where(t => !sizes.Contains(t)))
        {
            sizes.Add(t);
        }
            
        foreach (var t in filters)
        {
            if (!brands.Contains(t.Brand))
            {
                brands.Add(t.Brand);
            }
                
            if (!types.Contains(t.ShoeType))
            {
                types.Add(t.ShoeType);
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