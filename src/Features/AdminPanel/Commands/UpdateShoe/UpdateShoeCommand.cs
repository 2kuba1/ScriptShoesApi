using MediatR;

namespace ScriptShoesCQRS.Features.AdminPanel.Commands.UpdateShoe;

public record UpdateShoeCommand : IRequest
{
    public string ShoeName { get; set; }
    public string NewName { get; set; }
    public float? PreviousPrice { get; set; }
    public double CurrentPrice { get; set; }
    public string Brand { get; set; }
    public string ShoeType { get; set; }
    public string SizesList { get; set; }
}