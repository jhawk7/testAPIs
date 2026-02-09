using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
  private TodoDbContext _dbContext;
  public TodoController(TodoDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<TodoItem>>> GetItems()
  {
    var list =  await _dbContext.TodoItems.ToListAsync();
    return Ok(list);
  }

  [HttpGet("{id:int:min(1)}")]
  public async Task<ActionResult<TodoItem>> GetItem(int id)
  {
    var item = await _dbContext.TodoItems.FindAsync(id);
    if (item == null)
    {
      return NotFound();
    }
    return Ok(item);
  }

  [HttpPost]
  public async Task<ActionResult<TodoItem>> CreateItem(TodoItem item)
  {
    _dbContext.Add(item);
    await _dbContext.SaveChangesAsync();
    return CreatedAtAction(nameof(GetItem), new {id = item.Id}, item);
  }

  [HttpDelete("{id:int:min(1)}")]
  public async Task<ActionResult> DeleteItem(int id)
  {
    var item = await _dbContext.TodoItems.FindAsync(id);
    if (item != null)
    {
      _dbContext.TodoItems.Remove(item);
      await _dbContext.SaveChangesAsync();
    }

    return NoContent();
  }
}