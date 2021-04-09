using SQLite;

namespace Regiotran.Models
{
    public class Login
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Number { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
    }
}