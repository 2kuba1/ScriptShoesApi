using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesApi.Exceptions;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Features.Users.Tokens;
using ScriptShoesCQRS.Models.Users;

namespace ScriptShoesCQRS.Features.Users.Queries.RefreshToken;

public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, AuthenticationUserResponse>
{
    private readonly AppDbContext _dbContext;
    private readonly ITokensMethods _tokensMethods;

    public RefreshTokenQueryHandler(AppDbContext dbContext, ITokensMethods tokensMethods)
    {
        _dbContext = dbContext;
        _tokensMethods = tokensMethods;
    }
    
    public async Task<AuthenticationUserResponse> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken, cancellationToken: cancellationToken);

        if (user is null)
        {
            throw new NotFoundException("Invalid refresh token");
        }

        if (user.TokenExpires < DateTime.Now)
        {
            throw new NotFoundException("Token has expired");
        }

        var accessToken = _tokensMethods.CreateToken(user);
        var newRefreshToken = _tokensMethods.CreateRefreshToken();
        await _tokensMethods.SetRefreshToken(newRefreshToken, user);

        return new AuthenticationUserResponse
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken.Token,
            TokenExpires = newRefreshToken.Expires
        };
    }
}