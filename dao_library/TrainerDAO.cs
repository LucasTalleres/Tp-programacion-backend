using dao_library.entity_framework;
using entity_library;

namespace dao_library;

public class TrainerDAO : EntityFrameworkDao<Trainer>
{
    public TrainerDAO(AppDbContext context) : base(context, (item, id) => item.Id = id) { }
}
