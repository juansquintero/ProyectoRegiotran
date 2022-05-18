using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Regiotran.Models;

namespace Regiotran.Helpers
{
    public class FirebaseHelper
    {
        private readonly string ChildName = "Persons";

        readonly FirebaseClient firebase = new FirebaseClient("https://regiotram-67590-default-rtdb.firebaseio.com/");

        public static string IdGen()
        {
            Random ran = new Random();
            String b = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int le = 5;
            String random = "";
            for (int i = 0; i < le; i++)
            {
                int a = ran.Next(26);
                random = random + b.ElementAt(a);
            }
            return random;
        }
        public async Task<List<Login>> GetAllPersons()
        {
            return (await firebase
                .Child(ChildName)
                .OnceAsync<Login>()).Select(item => new Login
                {
                    Name = item.Object.Name,
                    Id = item.Object.Id,
                    Number = item.Object.Number,
                    Password = item.Object.Password,
                    Tickets = item.Object.Tickets,
                    Rol = item.Object.Rol
                }).ToList();
        }

        public async Task<Login> Login(string number, string password)
        {
            var allPersons = await GetAllPersons();
            await firebase
                .Child(ChildName)
                .OnceAsync<Login>();
            return allPersons.FirstOrDefault(a => a.Number == number && a.Password == password);
        }
        public async Task AddPerson(string number, string name,string password, int tickets, string admincode)
        {
            await firebase
                .Child(ChildName)
                .PostAsync(new Login() { Id = IdGen(), Number = number, Name = name, Password = password, Tickets = tickets, Rol = admincode });
        }

        public async Task<Login> GetById(string id)
        {
            var allPersons = await GetAllPersons();
            await firebase
                .Child(ChildName)
                .OnceAsync<Login>();
            return allPersons.FirstOrDefault(a => a.Id == id);
        }

        public async Task<Login> GetNumber(string number)
        {
            var allPersons = await GetAllPersons();
            await firebase
                .Child(ChildName)
                .OnceAsync<Login>();
            return allPersons.FirstOrDefault(a => a.Number == number);
        }

        public async Task UpdatePerson(string personId, string name, string phone, string rol, string password, int tickets)
        {
            var toUpdatePerson = (await firebase
                .Child(ChildName)
                .OnceAsync<Login>()).FirstOrDefault(a => a.Object.Id == personId);

            await firebase
                .Child(ChildName)
                .Child(toUpdatePerson.Key)
                .PutAsync(new Login() { Id = personId, Name = name, Number = phone, Rol = rol, Password = password, Tickets = tickets});
        }

        public async Task AddTicket(string personid, string name, string number, string password, string rol, int ticket)
        {
            var toUpdatePerson = (await firebase
                .Child(ChildName)
                .OnceAsync<Login>()).FirstOrDefault(a => a.Object.Number == number);

            await firebase
                .Child(ChildName)
                .Child(toUpdatePerson.Key)
                .PutAsync(new Login() { Id = personid, Name = name, Number = number, Password = password, Rol = rol, Tickets = ticket });
        }

        public async Task DeletePerson(string personId)
        {
            var toDeletePerson = (await firebase
                .Child(ChildName)
                .OnceAsync<Login>()).FirstOrDefault(a => a.Object.Id == personId);
            await firebase.Child(ChildName).Child(toDeletePerson.Key).DeleteAsync();
        }
    }
}
