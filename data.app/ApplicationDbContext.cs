using data.app.Models;
using Microsoft.EntityFrameworkCore;

namespace ServerSide.Api.Models
{
    public class ApplicationDbContext:DbContext
    {
       
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }
        public DbSet<Product> Product { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
