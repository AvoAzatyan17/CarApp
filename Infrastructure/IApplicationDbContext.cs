using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public interface IApplicationDbContext
{
    DbSet<Car> Cars { get; set; }
    DbSet<Mark> Marks { get; set; }
}