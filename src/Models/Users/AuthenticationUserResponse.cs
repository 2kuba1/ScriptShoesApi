namespace ScriptShoesAPI.Models.Users;

public class AuthenticationUserResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime TokenExpires { get; set; } = DateTime.Now;
}