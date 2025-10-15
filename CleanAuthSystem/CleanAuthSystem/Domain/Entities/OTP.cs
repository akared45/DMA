namespace CleanAuthSystem.Domain.Entities
{
    public class OTP
    {
        public string Code { get; set; }
        public DateTime Expiry { get; set; }
        public OTP(string code, DateTime expiry)
        {
            Code = code;
            Expiry = expiry;
        }
    }
}
