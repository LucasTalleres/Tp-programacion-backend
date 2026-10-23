using System.ComponentModel.DataAnnotations;

namespace entity_library;

public class Team
{
    public long Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = "";

    public string Category { get; set; } = "";

    [MinLength(1, ErrorMessage = "A team must have at least one player.")]
    public List<Player> Players { get; set; } = new();
}
