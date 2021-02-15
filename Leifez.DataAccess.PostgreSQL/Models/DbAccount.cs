namespace Leifez.DataAccess.PostgreSQL.Models
{
    public class DbAccount
    {
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
