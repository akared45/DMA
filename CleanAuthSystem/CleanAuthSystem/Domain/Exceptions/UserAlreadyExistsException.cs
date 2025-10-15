namespace CleanAuthSystem.Domain.Exceptions
{
    public class UserAlreadyExistsException : DomainException
    {
        public UserAlreadyExistsException() : base("Username already exists") { }
    }
}
