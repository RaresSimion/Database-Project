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
    public class DrinkDAO : BaseDao
    {
        public List<Drink> GetAllDrinks()
        {
            // select columns from database
            string query = "SELECT Drink_number,Drink_name,IsAlcoholic,Drink_price,Drink_stock FROM [Drink]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
        private List<Drink> ReadTables(DataTable dataTable)
        {
            //create list to store the rooms 
            List<Drink> drinks = new List<Drink>();

            foreach (DataRow dr in dataTable.Rows)
            {
                //store each room with the following fields from the database
                Drink drink = new Drink()
                {
                    Number = (int)(dr["Drink_number"]),
                    Name = (string)(dr["Drink_name"]),
                    IsAlcoholic = (bool)(dr["IsAlcoholic"]),
                    Price = (double)(dr["Drink_price"]),
                    Stock = (int)(dr["Drink_stock"])

                };
                drinks.Add(drink);
            }
            return drinks;
        }
    }
}
