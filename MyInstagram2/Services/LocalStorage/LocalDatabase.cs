using MyInstagram2.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyInstagram2.Services.LocalStorage
{
   public  class LocalDatabase
    {
        readonly SQLiteAsyncConnection _database;
        

        public LocalDatabase()
        {
            _database = new SQLiteAsyncConnection(RestUri.path);
            _database.CreateTableAsync<DatabaseModel>();
        }

        public async Task saveToken(AutenticatedUser model)
        {
            DatabaseModel user = new DatabaseModel()
            {
                LoginAt = DateTime.Today,
                Email = model.Email,
                Username = model.Username,
                IsAuthenticated = model.IsAuthenticated,
                Token = model.Token,
                Role = model.Roles[0]
            };

            await _database.InsertAsync(user);
        }

        public async Task<AutenticatedUser> GetTokenFromLocal()
        {
            AutenticatedUser user = new AutenticatedUser();

            var token = await _database.Table<DatabaseModel>().FirstOrDefaultAsync();
            if(token == null)
            {
                return null;
            }

            /*if(token.LoginAt.Date != DateTime.Today.Date)
            {
                await _database.DeleteAsync(token);
                user.IsAuthenticated = false;
                user.Message = "Token Expired, please relogin";
                return user;
            }*/

            user.Token = token.Token;
            user.Email = token.Email;
            user.IsAuthenticated = token.IsAuthenticated;
            user.Username = token.Username;
            user.Roles.Add(token.Role);

            return user;
            
        }

        public async Task DeleteToken()
        {
            await _database.DeleteAllAsync<DatabaseModel>();
        }
    }
}
