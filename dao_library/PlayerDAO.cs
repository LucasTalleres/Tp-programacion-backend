using dao_library.entity_framework;
using entity_library;

namespace dao_library;

public class PlayerDAO : EntityFrameworkDao<Player>
{
    public PlayerDAO(AppDbContext context) : base(context, (item, id) => item.Id = id) { }
}
