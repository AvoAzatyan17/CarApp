namespace Domain.Entities
{
    public class BaseEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string CreatedBy { get; set; }
        
    }
}