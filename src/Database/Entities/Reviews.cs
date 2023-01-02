namespace ScriptShoesAPI.Database.Entities
{
    public class Reviews
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Review { get; set; }
        public int Rate { get; set; }
        public int ReviewLikes { get; set; }
        public string Title { get; set; }
        public int ShoesId { get; set; }
        public int UserId { get; set; }
        public virtual User Users { get; set; }
    }
}
