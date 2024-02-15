namespace RestWithASP_NET8Udemy.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public DateTime ExpirationToken { get; set; }
    }
}
