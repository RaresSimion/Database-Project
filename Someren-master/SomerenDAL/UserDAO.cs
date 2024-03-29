﻿using System;
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
        //adding a new user to the db
        public void AddToRegister(string username, string password, string salt, bool role)
        {

            string query = $"INSERT INTO [User] (Username, Password, Salt, Role) VALUES ('{username}', '{password}', '{salt}', '{role}')";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query, sqlParameters);
        }

        //getting the user from the db by the username, in order to get the salt
        public User GetUserByUsername(string username)
        {
            string query = $"SELECT Username, Password, Salt, Role FROM [User] WHERE Username ='{username}'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTable(ExecuteSelectQuery(query, sqlParameters));
        }

        //getting a list of all the users
        public List<User> GetAllUsers()
        {
            string query = $"SELECT Username, Password, Salt, Role FROM [User]";
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
                    Salt = (string)(dr["Salt"]),
                    Role = (bool)(dr["Role"])
                };
                users.Add(user);
            }
            return users;
        }

        private User ReadTable(DataTable dataTable)
        {
            // create object to store values
            User user = new User();
            if (dataTable.Rows.Count > 0)
            {
                DataRow dr = dataTable.Rows[0];
                user.Username = (string)(dr["Username"]);
                user.Password = (string)(dr["Password"]);
                user.Salt = (string)(dr["Salt"]);
                user.Role = (bool)(dr["Role"]);
            }
            else
            {
                throw new Exception("There is no user with these credentials");
            }
            return user;
        }
    }
}
