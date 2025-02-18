using Microsoft.EntityFrameworkCore;
using TodoAppEntityFramework.Models;

namespace TodoAppEntityFramework.Data
{
    public class AppDbContext :DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
