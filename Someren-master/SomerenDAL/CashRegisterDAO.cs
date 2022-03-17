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
        public void AddToRegister(int studentNumber, int drinkNumber)
        {
            // add student number and drink numbers to the database
            string query = $"INSERT into CashRegister (Student_number,Drink_number) VALUES {studentNumber}, {drinkNumber}";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query, sqlParameters);
        }   
    }
}
