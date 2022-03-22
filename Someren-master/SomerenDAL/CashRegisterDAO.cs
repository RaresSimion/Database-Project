using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenModel;

namespace SomerenDAL
{
    public class CashRegisterDAO:BaseDao
    {
        public void AddToRegister(int studentNumber, int drinkNumber, DateTime orderDate)
        {
            // add student number, drink number and order date to the database
            string query = $"INSERT INTO Cash_register (Student_number, Drink_number, Order_date) VALUES ({studentNumber}, {drinkNumber}, '{orderDate:yyyy-MM-dd}')";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query, sqlParameters);
        }   
    }
}
