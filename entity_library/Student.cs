namespace entity_library;

public class Student : Person
{
    public string File { get; set; } = "";
    public List<Course> Courses { get; set; } = new();
}
