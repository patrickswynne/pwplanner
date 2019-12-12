using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WGUPlanner.Models;
using WGUPlanner.Interface;
using WGUPlanner.Implementation;

namespace WGUPlanner
{
    public class LoginRepository
    {
        private BusinessImplementation bi = new BusinessImplementation();
        public static bool UserExistsState { get; set; }
        // A PRIVATE CONNECTION STRING FOR THIS REPOSITORY
        private SQLiteAsyncConnection conn;
        //INJECT THE DBPATH FROM THE 
        public LoginRepository(string dbPath) 
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Users>().Wait();
        }
        // 
        // CHECK IF PROVIDED CREDENTIALS MATCH THE USER IN THE LOGIN REPO
        //
        public Task GetUserLoginAsync(Users user)
        {
            if (bi.validUser(user))
            {
                return conn.Table<Users>().Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefaultAsync();
            }
            else
            {
                return null;
            }
        }
        // 
        // REGISTER A USER WITH THE PROVIDED CREDENTIALS
        //
        public async Task AddNewUserAsync(Users user)
        {

            int result = 0;
            try
            {
                if (bi.validUser(user))
                {
                    result = await conn.InsertAsync(user);
                    Console.WriteLine(string.Format("{0} record(s) added [Name: {1})", result, user.Email));
                }
                else
                {
                    Console.WriteLine(string.Format("Failed to add {0}. Error: {1}", user.Email, "invalid user"));
                }
                
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Format("Failed to add {0}. Error: {1}", user.Email, exception.Message));
                Console.WriteLine(exception.ToString());
            }
        }
        // 
        // CHECK IF ANY USER EXISTS IN THE LOGIN REPO
        //
        public bool hasUsers()
        {
            try
            {
                conn.Table<Users>().FirstAsync();
                UserExistsState = true;
                return true;
            }
            catch (NullReferenceException nullReferenceException)
            {
                // we didn't find a user, continue on to the registration screen
                Console.WriteLine(nullReferenceException.ToString());
                UserExistsState = false;
                return false;
            }
            catch (AggregateException aggregateException)
            {
                // we didn't find a user, continue on to the registration screen
                Console.WriteLine(aggregateException.ToString());
                UserExistsState = false;
                return false;
            }
            finally
            {
                // 
            }
        }
    }
}
