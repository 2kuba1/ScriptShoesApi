namespace ScriptShoesAPI.Database.Entities;

public class EmailCodes
{
    public int Id { get; set; }
    public string GeneratedCode { get; set; }
    public DateTime CodeCreated { get; set; } = DateTime.Now;
    public DateTime CodeExpires { get; set; }
    public int UserId { get; set; }
}