namespace CleanAuthSystem.Domain.Exceptions
{
    public class OTPExpiredException : DomainException
    {
        public OTPExpiredException() : base("The OTP has expired.") { }
    }
}
