
using Auth.Domain.Common;
using Shared;

namespace Auth.Domain.Users
{
    public class User : BaseEntity
    {
        public Email Email { get; private set; }
        public Status Status { get; private set; }

        public User(Guid id,Email email)
        {
            Id = id;
            Email = email;
            Status = new Status();
        }

        public User(
            Guid id, 
            DateTime createdAt, 
            DateTime updatedAt,
            Email email, 
            bool isBanned, 
            DateTime? suspentionEnd ) : base(id, createdAt, updatedAt)
        {
            Email = email;
            Status = new Status(isBanned, suspentionEnd);
        }


        public bool IsSuspended()
        {
            if (!Status.SuspentionEnd.HasValue)
                return false;
            return DateTime.UtcNow < Status.SuspentionEnd.Value;
        }

        public void Suspend(TimeSpan duration)
        {
            if (Status.IsBanned)
                throw new DomainException("Cannot suspend a banned user.");
            Status.SuspendUntil(DateTime.UtcNow.Add(duration));
        }

        public void Ban()
        {
            Status.Ban();
        }

    }
}
