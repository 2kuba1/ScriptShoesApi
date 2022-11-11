using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ScriptShoesApi.Entities;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Models.Users;

namespace ScriptShoesCQRS.Features.Users.Tokens;

public class TokensMethods : ITokensMethods
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDbContext _dbContext;

    public TokensMethods(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    public RefreshToken CreateRefreshToken()
    {
        var refreshToken = new RefreshToken()
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddHours(_configuration.GetValue<int>("Jwt:RefreshKeyExpireHours")),
            Created = DateTime.Now
        };

        return refreshToken;
    }

    public async Task SetRefreshToken(RefreshToken refreshToken, User user)
    {
        var cookieOptions = new CookieOptions()
        {
            HttpOnly = true,
            Expires = refreshToken.Expires
        };

        _httpContextAccessor?.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken.Token,
            cookieOptions);

        user.RefreshToken = refreshToken.Token;
        user.TokenCreated = refreshToken.Created;
        user.TokenExpires = refreshToken.Expires;

        await _dbContext.SaveChangesAsync();
    }

    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("Username", user.Username),
            new Claim("Name", $"{user.Name}"),
            new Claim("Surname", $"{user.Surname}"),
            new Claim("Email", $"{user.Email}"),
            new Claim("ProfilePicture", user.ProfilePictureUrl),
            new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
        };
            
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_configuration.GetValue<string>("Jwt:Issuer"),
            _configuration.GetValue<string>("Jwt:Audience"),
            claims: claims,
            DateTime.Now,
            DateTime.Now.AddMinutes(_configuration.GetValue<int>("Jwt:ExpireMinutes")),
            credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}