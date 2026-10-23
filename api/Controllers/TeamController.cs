using dao_library;
using entity_library;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamController : CrudControllerBase<Team>
{
    public TeamController(TeamDAO dao) : base(dao) { }
    protected override long GetId(Team item) => item.Id;
}
