using System.Security.Claims;

namespace ScriptShoesAPI.Services.UserContext;

public interface IUserContextService
{
    ClaimsPrincipal User { get; }
    int? GetUserId { get; }
}