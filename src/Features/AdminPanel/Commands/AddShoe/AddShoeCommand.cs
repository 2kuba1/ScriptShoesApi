using MediatR;

namespace ScriptShoesAPI.Features.AdminPanel.Commands.AddShoe;

public record AddShoeCommand : IRequest<int>
{
    public string Brand { get; set; }
    public string Name { get; set; }
    public double CurrentPrice { get; set; }
    public string ShoeType { get; set; }

    public string SizesList { get; set; }
}