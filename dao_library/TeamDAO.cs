using dao_library.entity_framework;
using entity_library;
using Microsoft.EntityFrameworkCore;

namespace dao_library;

public class TeamDAO : EntityFrameworkDao<Team>
{
    public TeamDAO(AppDbContext context) : base(context, (item, id) => item.Id = id) { }

    public override IReadOnlyList<Team> GetAll() => Context.Teams
        .AsNoTracking()
        .Include(team => team.Players)
        .ToList();

    public override Team? GetById(long id) => Context.Teams
        .AsNoTracking()
        .Include(team => team.Players)
        .SingleOrDefault(team => team.Id == id);

    public override Team Save(Team item)
    {
        List<Player> players = ResolvePlayers(item.Players);
        item.Players = players;
        Context.Teams.Add(item);
        Context.SaveChanges();
        return item;
    }

    public override Team? Update(long id, Team item)
    {
        Team? current = Context.Teams
            .Include(team => team.Players)
            .SingleOrDefault(team => team.Id == id);

        if (current == null)
        {
            return null;
        }

        List<Player> players = ResolvePlayers(item.Players);
        current.Name = item.Name;
        current.Category = item.Category;
        current.Players.Clear();
        foreach (Player player in players)
        {
            current.Players.Add(player);
        }

        Context.SaveChanges();
        Context.ChangeTracker.Clear();
        return GetById(id);
    }

    private List<Player> ResolvePlayers(IEnumerable<Player> requestedPlayers)
    {
        long[] ids = requestedPlayers.Select(player => player.Id).Distinct().ToArray();
        if (ids.Length == 0)
        {
            throw new InvalidOperationException("A team must have at least one player.");
        }

        if (ids.Any(id => id <= 0))
        {
            throw new InvalidOperationException("Every team member must reference an existing player by id.");
        }

        List<Player> players = Context.Players.Where(player => ids.Contains(player.Id)).ToList();
        long[] missingIds = ids.Except(players.Select(player => player.Id)).ToArray();
        if (missingIds.Length > 0)
        {
            throw new KeyNotFoundException($"Player not found: {string.Join(", ", missingIds)}.");
        }

        return players;
    }
}
