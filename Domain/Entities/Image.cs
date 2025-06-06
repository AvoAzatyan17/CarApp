namespace Domain.Entities;

public class Image: BaseEntity
{
    public int Id { get; set; }

    public Guid CarId { get; set; }
    public Car Car { get; set; } = null!;

    public string ObjectPath { get; set; } = null!;
}