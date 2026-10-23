namespace entity_library;

public class Activity
{
    public long Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime Date { get; set; }
    public TypeActivity TypeActivity { get; set; }
}
