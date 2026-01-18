using Auth.Domain.Common;


namespace Auth.Domain.Users
{
    public class Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Invalid email");

            if (!value.Contains("@"))
                throw new DomainException("Invalid email");

            Value = value.ToLowerInvariant();
        }

        public override bool Equals(object? obj)
        => obj is Email other && Equals(other);

        public bool Equals(Email? other)
            => other is not null && Value == other.Value;

        public override int GetHashCode()
            => Value.GetHashCode();

        public static bool operator ==(Email? left, Email? right)
            => Equals(left, right);

        public static bool operator !=(Email? left, Email? right)
            => !Equals(left, right);


        public override string ToString() => Value;
    }

}
