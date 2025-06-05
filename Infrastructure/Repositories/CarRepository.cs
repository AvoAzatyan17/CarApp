using Domain.Entities;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CarRepository: ICarRepository
{
    private readonly ApplicationDbContext _context;

    public CarRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Car>> GetAllAsync()
    {
        return await _context.Cars
            .Include(c => c.Mark)
            .ToListAsync();
    }
}