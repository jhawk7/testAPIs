using Microsoft.EntityFrameworkCore;

namespace DotnetAPI.Models;

public class TodoDbContext : DbContext
{
  public TodoDbContext(DbContextOptions<TodoDbContext> options)
    : base(options)
  {
  }

 public DbSet<TodoItem> TodoItems { get; set; } = null!;
}