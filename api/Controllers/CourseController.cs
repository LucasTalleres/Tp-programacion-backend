using dao_library;
using entity_library;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class CourseController : CrudControllerBase<Course>
{
    public CourseController(CourseDAO dao) : base(dao) { }
    protected override long GetId(Course item) => item.Id;
}
