using dao_library;
using entity_library;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class TrainerController : CrudControllerBase<Trainer>
{
    public TrainerController(TrainerDAO dao) : base(dao) { }
    protected override long GetId(Trainer item) => item.Id;
}
