using System.Security.Claims;

namespace ScriptShoesCQRS.Services.UserContext;

public interface IUserContextService
{
    ClaimsPrincipal User { get; }
    int? GetUserId { get; }
}