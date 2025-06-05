using Domain.Entities;

namespace Domain.Interface;

public interface ICarRepository
{
    Task<List<Car>> GetAllAsync();
}