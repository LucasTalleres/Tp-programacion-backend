using System.ComponentModel.DataAnnotations;

namespace entity_library;

public class Course
{
    public long Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = "";

    [MinLength(1, ErrorMessage = "A course must have at least one student.")]
    public List<Student> Students { get; set; } = new();
    public List<Activity> Activities { get; set; } = new();
}
