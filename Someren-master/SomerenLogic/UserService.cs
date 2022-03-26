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
        public void GetUser(User user)
        {
            //update the activity stock in the database
            userDb.GetUser(user);
        }
        public List<User> GetAllUsers()
        {
            List<User> users = userDb.GetAllUsers();
            return users;
        }
    }
}
