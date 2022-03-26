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

    }
}
