using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext ,IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Image> Image { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
    
    
}