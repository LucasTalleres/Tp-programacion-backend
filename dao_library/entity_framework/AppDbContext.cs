using entity_library;
using Microsoft.EntityFrameworkCore;

namespace dao_library.entity_framework;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Person> People { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Activity> Activities { get; set; }

    public override int SaveChanges()
    {
        ValidateMinimumCardinalities();
        return base.SaveChanges();
    }

    private void ValidateMinimumCardinalities()
    {
        foreach (var entry in ChangeTracker.Entries<Team>()
                     .Where(entry => entry.State is EntityState.Added or EntityState.Modified))
        {
            if (entry.State == EntityState.Modified && !entry.Collection(team => team.Players).IsLoaded)
            {
                entry.Collection(team => team.Players).Load();
            }

            if (entry.Entity.Players.Count == 0)
            {
                throw new InvalidOperationException("A team must have at least one player.");
            }
        }

        foreach (var entry in ChangeTracker.Entries<Course>()
                     .Where(entry => entry.State is EntityState.Added or EntityState.Modified))
        {
            if (entry.State == EntityState.Modified && !entry.Collection(course => course.Students).IsLoaded)
            {
                entry.Collection(course => course.Students).Load();
            }

            if (entry.Entity.Students.Count == 0)
            {
                throw new InvalidOperationException("A course must have at least one student.");
            }
        }

        foreach (Player player in ChangeTracker.Entries<Player>()
                     .Where(entry => entry.State == EntityState.Deleted)
                     .Select(entry => entry.Entity))
        {
            bool wouldLeaveTeamEmpty = Teams
                .Any(team => team.Players.Any(member => member.Id == player.Id)
                             && team.Players.Count(member => member.Id != player.Id) == 0);

            if (wouldLeaveTeamEmpty)
            {
                throw new InvalidOperationException("The player cannot be deleted because a team must have at least one player.");
            }
        }

        foreach (Student student in ChangeTracker.Entries<Student>()
                     .Where(entry => entry.State == EntityState.Deleted)
                     .Select(entry => entry.Entity))
        {
            bool wouldLeaveCourseEmpty = Courses
                .Any(course => course.Students.Any(member => member.Id == student.Id)
                               && course.Students.Count(member => member.Id != student.Id) == 0);

            if (wouldLeaveCourseEmpty)
            {
                throw new InvalidOperationException("The student cannot be deleted because a course must have at least one student.");
            }
        }
    }
}
