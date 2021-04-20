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

        public async Task<List<Login>> GetAllPersons()
        {
            return (await firebase
                .Child(ChildName)
                .OnceAsync<Login>()).Select(item => new Login
                {
                    Name = item.Object.Name,
                    Id = item.Object.Id,
                    Number = item.Object.Number,
                    Password = item.Object.Password
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
        public async Task AddPerson(string number, string name,string password, string tickets)
        {
            await firebase
                .Child(ChildName)
                .PostAsync(new Login() { Id = Guid.NewGuid(), Number = number, Name = name, Password = password, Tickets = tickets });
        }

        //public async Task<Login> GetPerson(Guid id)
        //{
        //    var allPersons = await GetAllPersons();
        //    await firebase
        //        .Child(ChildName)
        //        .OnceAsync<Login>();
        //    return allPersons.FirstOrDefault(a => a.Id == id);
        //}

        public async Task<Login> GetNumber(string number)
        {
            var allPersons = await GetAllPersons();
            await firebase
                .Child(ChildName)
                .OnceAsync<Login>();
            return allPersons.FirstOrDefault(a => a.Number == number);
        }

        public async Task UpdatePerson(Guid personId, string name, string phone)
        {
            var toUpdatePerson = (await firebase
                .Child(ChildName)
                .OnceAsync<Login>()).FirstOrDefault(a => a.Object.Id == personId);

            await firebase
                .Child(ChildName)
                .Child(toUpdatePerson.Key)
                .PutAsync(new Login() { Id = personId, Name = name, Number = phone });
        }

        public async Task DeletePerson(Guid personId)
        {
            var toDeletePerson = (await firebase
                .Child(ChildName)
                .OnceAsync<Login>()).FirstOrDefault(a => a.Object.Id == personId);
            await firebase.Child(ChildName).Child(toDeletePerson.Key).DeleteAsync();
        }
    }
}
