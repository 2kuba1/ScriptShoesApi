using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesApi.Exceptions;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Features.Users.Tokens;
using ScriptShoesCQRS.Models.Users;

namespace ScriptShoesCQRS.Features.Users.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationUserResponse>
{
    private readonly AppDbContext _dbContext;
    private readonly ITokensMethods _tokensMethods;

    public LoginQueryHandler(AppDbContext dbContext, ITokensMethods tokensMethods)
    {
        _dbContext = dbContext;
        _tokensMethods = tokensMethods;
    }
    
    public async Task<AuthenticationUserResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken: cancellationToken);

        if (user is null)
        {
            throw new NotFoundException("Username or password is incorrect");
        }

        var result = Verify(request.Password, user.HashedPassword);

        if (result == false)
        {
            throw new NotFoundException("Username or password is incorrect");
        }

        var accessToken = _tokensMethods.CreateToken(user);
        var refreshToken = _tokensMethods.CreateRefreshToken();
        await _tokensMethods.SetRefreshToken(refreshToken, user);

        return new AuthenticationUserResponse()
        {
            AccessToken = accessToken,
            TokenExpires = refreshToken.Expires,
            RefreshToken = refreshToken.Token
        };
    }
}