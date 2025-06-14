using Domain.Entities;

namespace Domain.Interface;

public interface ICarRepository
{
    Task<List<Car>> GetAllAsync();
    Task<bool> SoftDeleteAsync(Guid id);

}