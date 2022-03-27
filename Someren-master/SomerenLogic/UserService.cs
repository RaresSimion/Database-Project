using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenDAL;
using SomerenModel;

namespace SomerenLogic
{
    public class UserService
    {
        UserDAO userDb;

        public UserService()
        {
            //create connection to database
            userDb = new UserDAO();
        }
        public void AddToRegister(string username, string password, string salt, bool role)
        {
            userDb.AddToRegister(username, password, salt, role);
        }
        public User GetUserByUsername(string username)
        {
           return userDb.GetUserByUsername(username);
        }
        public List<User> GetAllUsers()
        {
            List<User> users = userDb.GetAllUsers();
            return users;
        }
    }
}
