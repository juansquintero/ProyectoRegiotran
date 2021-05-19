using SQLite;
using System;

namespace Regiotran.Models
{
    public class Login
    {        
        public string Id { get; set; }
        public string Rol { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Tickets { get; set; }
    }
}