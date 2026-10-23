using dao_library;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public abstract class CrudControllerBase<T> : ControllerBase where T : class
{
    private readonly EntityFrameworkDao<T> dao;

    protected CrudControllerBase(EntityFrameworkDao<T> dao)
    {
        this.dao = dao;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<T>> GetAll() => Ok(dao.GetAll());

    [HttpGet("{id:long}")]
    public ActionResult<T> GetById(long id)
    {
        T? item = dao.GetById(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public ActionResult<T> Create(T item)
    {
        try
        {
            T createdItem = dao.Save(item);
            return Created($"{Request.Path}/{GetId(createdItem)}", createdItem);
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(new { error = exception.Message });
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(new { error = exception.Message });
        }
    }

    [HttpPut("{id:long}")]
    public IActionResult Update(long id, T item)
    {
        try
        {
            T? updatedItem = dao.Update(id, item);
            return updatedItem == null ? NotFound() : Ok(updatedItem);
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(new { error = exception.Message });
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(new { error = exception.Message });
        }
    }

    [HttpDelete("{id:long}")]
    public IActionResult Delete(long id)
    {
        try
        {
            return dao.DeleteById(id) ? NoContent() : NotFound();
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(new { error = exception.Message });
        }
    }

    protected abstract long GetId(T item);
}
