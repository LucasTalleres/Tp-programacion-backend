using dao_library;
using entity_library;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : CrudControllerBase<Student>
{
    public StudentController(StudentDAO dao) : base(dao) { }
    protected override long GetId(Student item) => item.Id;
}
