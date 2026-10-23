using dao_library;
using entity_library;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivityController : CrudControllerBase<Activity>
{
    public ActivityController(ActivityDAO dao) : base(dao) { }
    protected override long GetId(Activity item) => item.Id;
}
