using dao_library.entity_framework;
using entity_library;
using Microsoft.EntityFrameworkCore;

namespace dao_library;

public class CourseDAO : EntityFrameworkDao<Course>
{
    public CourseDAO(AppDbContext context) : base(context, (item, id) => item.Id = id) { }

    public override IReadOnlyList<Course> GetAll() => Context.Courses
        .AsNoTracking()
        .Include(course => course.Students)
        .Include(course => course.Activities)
        .ToList();

    public override Course? GetById(long id) => Context.Courses
        .AsNoTracking()
        .Include(course => course.Students)
        .Include(course => course.Activities)
        .SingleOrDefault(course => course.Id == id);

    public override Course Save(Course item)
    {
        List<Student> students = ResolveStudents(item.Students);
        item.Students = students;
        Context.Courses.Add(item);
        Context.SaveChanges();
        return item;
    }

    public override Course? Update(long id, Course item)
    {
        Course? current = Context.Courses
            .Include(course => course.Students)
            .SingleOrDefault(course => course.Id == id);

        if (current == null)
        {
            return null;
        }

        List<Student> students = ResolveStudents(item.Students);
        current.Name = item.Name;
        current.Students.Clear();
        foreach (Student student in students)
        {
            current.Students.Add(student);
        }

        Context.SaveChanges();
        Context.ChangeTracker.Clear();
        return GetById(id);
    }

    private List<Student> ResolveStudents(IEnumerable<Student> requestedStudents)
    {
        long[] ids = requestedStudents.Select(student => student.Id).Distinct().ToArray();
        if (ids.Length == 0)
        {
            throw new InvalidOperationException("A course must have at least one student.");
        }

        if (ids.Any(id => id <= 0))
        {
            throw new InvalidOperationException("Every course member must reference an existing student by id.");
        }

        List<Student> students = Context.Students.Where(student => ids.Contains(student.Id)).ToList();
        long[] missingIds = ids.Except(students.Select(student => student.Id)).ToArray();
        if (missingIds.Length > 0)
        {
            throw new KeyNotFoundException($"Student not found: {string.Join(", ", missingIds)}.");
        }

        return students;
    }
}
