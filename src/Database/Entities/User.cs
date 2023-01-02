namespace ScriptShoesAPI.Database.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public double AvailableFounds { get; set; } = 0;
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsActivated { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public string ProfilePictureUrl { get; set; }
        
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
        public int RoleId { get; set; }
        public virtual Roles Role { get; set; }
    }
}
