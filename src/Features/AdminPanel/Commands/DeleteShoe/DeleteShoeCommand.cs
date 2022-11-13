using MediatR;

namespace ScriptShoesCQRS.Features.AdminPanel.Commands.DeleteShoe;

public record DeleteShoeCommand : IRequest
{
    public string ShoeName { get; set; }
}