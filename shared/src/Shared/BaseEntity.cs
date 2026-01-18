 
namespace Shared
{
    public class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public BaseEntity(Guid id, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public void UpdateTimestamp()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        
    }
}
