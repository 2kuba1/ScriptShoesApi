using ScriptShoesAPI.Database.Entities;
using ScriptShoesAPI.Models.Users;

namespace ScriptShoesAPI.Features.Users.Tokens;

public interface ITokensMethods
{
    RefreshToken CreateRefreshToken();
    Task SetRefreshToken(RefreshToken refreshToken, User user);
    string CreateToken(User user);
}