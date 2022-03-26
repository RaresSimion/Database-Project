using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using SomerenModel;

namespace SomerenDAL
{
    public class UserDAO:BaseDao
    {
        public void AddToRegister(string username, string password, string salt)
        {

            string query = $"INSERT INTO Users_test (Username, Password, Salt) VALUES ('{username}', {password}, '{salt}')";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query, sqlParameters);
        }
        public void GetUser(User user)
        {
            string query = $"SELECT UserID, Username, Password, Role FROM [User] WHERE Username ='{user.Username}'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query, sqlParameters);
        }
        public List<User> GetAllUsers()
        {
            string query = $"SELECT UserID, Username, Password, Role FROM [User]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
        private List<User> ReadTables(DataTable dataTable)
        {
            //create list to store the activities 
            List<User> users = new List<User>();

            foreach (DataRow dr in dataTable.Rows)
            {
                //store each room with the following fields from the database
                User user = new User()
                {
                    Username = (string)(dr["Username"]),
                    Password = (string)(dr["Password"]),
                    Role = (bool)(dr["Role"])
                };
                users.Add(user);
            }
            return users;
        }

    }
}
