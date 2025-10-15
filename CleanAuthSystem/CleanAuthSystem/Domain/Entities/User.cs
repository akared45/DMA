namespace CleanAuthSystem.Domain.Entities
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool Verified { get; set; }
        public User(string username, string password, string phone, bool verified = false)
        {
            Username = username;
            Password = password;
            Phone = phone;
            Verified = verified;
        }
    }
}
