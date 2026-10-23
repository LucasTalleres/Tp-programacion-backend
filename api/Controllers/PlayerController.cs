using dao_library;
using entity_library;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController : CrudControllerBase<Player>
{
    public PlayerController(PlayerDAO dao) : base(dao) { }
    protected override long GetId(Player item) => item.Id;
}
