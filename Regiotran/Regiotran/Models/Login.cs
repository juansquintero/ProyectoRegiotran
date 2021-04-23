using SQLite;
using System;

namespace Regiotran.Models
{
    public class Login
    {        
        public Guid Id { get; set; }
        public string Rol { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Tickets { get; set; }
    }
}