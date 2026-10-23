using dao_library.entity_framework;
using entity_library;

namespace dao_library;

public class ActivityDAO : EntityFrameworkDao<Activity>
{
    public ActivityDAO(AppDbContext context) : base(context, (item, id) => item.Id = id) { }
}
