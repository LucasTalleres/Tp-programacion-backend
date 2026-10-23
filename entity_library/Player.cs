namespace entity_library;

public class Player : Person
{
    public int Number { get; set; }
    public List<Team> Teams { get; set; } = new();
}
