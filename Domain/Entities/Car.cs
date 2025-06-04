namespace Domain.Entities
{
    public class Car: BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public Guid MakeId { get; set; }
        public Mark Mark { get; set; }
        
    }
}