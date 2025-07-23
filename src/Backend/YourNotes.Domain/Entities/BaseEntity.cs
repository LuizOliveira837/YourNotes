namespace YourNotes.Domain.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            Active = true;
            CreatedOn = DateTime.Now;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}