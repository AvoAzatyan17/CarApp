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

    public async Task<bool> SoftDeleteAsync(Guid id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null)
        {
            return false;
        }
        car.IsDeleted = true;
        _context.Cars.Update(car);
        await _context.SaveChangesAsync();
        return true;
    }
}