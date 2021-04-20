using Regiotran.Models;
using SQLite;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Regiotran.Services
{
    public static class DataBase
    {
        private static SQLiteAsyncConnection db;

        private static async Task Init()
        {
            if (db != null)
                return;

            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Login.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Login>();
        }

        public static async Task AddData(string name, string number, string password)
        {
            await Init();
            var database = new Login
            {
                Name = name,                
                Number = number,
                Password = password,
                Tickets = "0"
            };
            var id = await db.InsertAsync(database);
        }

        public static async Task RemoveData(int id)
        {
            await Init();
            await db.DeleteAsync<Login>(id);
        }

        public static async Task<Login> GetNumberLogin(string number, string password)
        {
            await Init();
            var query = await db.Table<Login>().Where(v => v.Number.Equals(number) && v.Password.Equals(password)).FirstOrDefaultAsync();
            return query;            
        }
    }
}