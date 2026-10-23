using System.ComponentModel.DataAnnotations;

namespace entity_library;

public class Person
{
    public long Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = "";

    [Range(1, 100)]
    public int Age { get; set; }

    public string? Dni { get; set; }
}
