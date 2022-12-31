namespace ScriptShoesCQRS.Models.Shoes;

public class GetFiltersDto
{
    public List<string> Sizes { get; set; }
    public List<string> Brand { get; set; }
    public List<string> ShoeType { get; set; }
}