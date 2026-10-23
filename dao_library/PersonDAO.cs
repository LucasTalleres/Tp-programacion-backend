using dao_library.entity_framework;
using entity_library;

namespace dao_library;

public class PersonDAO : EntityFrameworkDao<Person>
{
    public PersonDAO(AppDbContext context) : base(context, (item, id) => item.Id = id) { }
}
