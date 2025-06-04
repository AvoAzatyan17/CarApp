namespace Domain.Entities
{
    public class Car: BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public Guid MarkId { get; set; }
        public Mark Mark { get; set; }
        
    }
}