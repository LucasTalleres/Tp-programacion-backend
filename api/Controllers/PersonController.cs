using dao_library;
using entity_library;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : CrudControllerBase<Person>
{
    public PersonController(PersonDAO dao) : base(dao) { }
    protected override long GetId(Person item) => item.Id;
}
