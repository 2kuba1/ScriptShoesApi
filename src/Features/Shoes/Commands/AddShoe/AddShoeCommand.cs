using MediatR;

namespace ScriptShoesCQRS.Features.Shoes.Commands.AddShoe;

public class AddShoeCommand : IRequest<int>
{
    public string Brand { get; set; }
    public string Name { get; set; }
    public double CurrentPrice { get; set; }
    public string MainImg { get; set; }
    public string ShoeType { get; set; }

    public string SizesList { get; set; }
}