using dao_library.entity_framework;
using entity_library;

namespace dao_library;

public class StudentDAO : EntityFrameworkDao<Student>
{
    public StudentDAO(AppDbContext context) : base(context, (item, id) => item.Id = id) { }
}
