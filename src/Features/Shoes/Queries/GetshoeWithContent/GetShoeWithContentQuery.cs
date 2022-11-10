using MediatR;
using ScriptShoesCQRS.Models.Shoes;

namespace ScriptShoesCQRS.Features.Shoes.Queries.GetShoeWithContent;

public class GetShoeWithContentQuery : IRequest<GetShoeWithContentResponse>
{
    public string ShoeName { get; set; }
}