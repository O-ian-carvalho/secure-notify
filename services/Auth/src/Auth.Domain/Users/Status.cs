
using Auth.Domain.Common;

namespace Auth.Domain.Users
{
    public class Status
    {
        public Status(bool isBanned, DateTime? suspentionEnd)
        {
            IsBanned = isBanned;
            SuspentionEnd = suspentionEnd;
        }

        public Status()
        {
            IsBanned = false;
        }

        public bool IsBanned { get; private set; }
        public DateTime? SuspentionEnd { get; private set; }

        public void SuspendUntil(DateTime time)
        {
            if (IsBanned)
                throw new DomainException("Cannot suspend a banned user.");
            SuspentionEnd = time;
        }

        public void Ban()
        {
            IsBanned = true;
            SuspentionEnd = null;
        }
    }
}
