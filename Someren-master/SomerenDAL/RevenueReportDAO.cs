using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using SomerenModel;
using System.Configuration;


namespace SomerenDAL
{
    public class RevenueReportDAO : BaseDao
    {
        public RevenueReport GetReport(DateTime startDate, DateTime endDate)
        {
            // select columns from database
            string query = $"SELECT COUNT(C.Order_id) AS [Number of drinks ordered], SUM(D.Drink_price) AS [Total profit], COUNT(DISTINCT C.Student_number) AS [Number of customers] FROM Cash_register_test AS C JOIN Drink AS D ON C.Drink_number=D.Drink_number WHERE C.Order_date>='{startDate:yyyy-MM-dd}' AND C.Order_date<='{endDate:yyyy-MM-dd}'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTable(ExecuteSelectQuery(query, sqlParameters));
        }

        private RevenueReport ReadTable(DataTable dataTable)
        {
            // create object to store values
            RevenueReport revenueReport = new RevenueReport();
            DataRow dr = dataTable.Rows[0];

            revenueReport.NumberOfDrinks = (int)(dr["Number of drinks ordered"]);
            revenueReport.Turnover = (double)(dr["Total profit"]);
            revenueReport.NumberOfCustomers = (int)(dr["Number of customers"]);
            
            return revenueReport;
        }
    }
}
