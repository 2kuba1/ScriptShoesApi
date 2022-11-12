using ScriptShoesApi.Entities;
using ScriptShoesCQRS.Models.Users;

namespace ScriptShoesCQRS.Features.Users.Tokens;

public interface ITokensMethods
{
    RefreshToken CreateRefreshToken();
    Task SetRefreshToken(RefreshToken refreshToken, User user);
    string CreateToken(User user);
}