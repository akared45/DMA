namespace CleanAuthSystem.Domain.Exceptions
{
    public class UserNotVerifiedException : DomainException
    {
        public UserNotVerifiedException() : base("User is not verified.") { }
    }
}
