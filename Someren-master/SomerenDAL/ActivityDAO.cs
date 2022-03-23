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
    public class ActivityDAO:BaseDao
    {
        public List<Activity> GetAllActivities()
        {
            // select columns from database
            // string query = "SELECT ActivityID,Activity_name,ActicityStartDateTime,ActivityEndDateTime FROM Activity";
            string query = "SELECT Activity_id, Activity_name, Activity_start_datetime, Activity_end_datetime FROM Activity";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
        private List<Activity> ReadTables(DataTable dataTable)
        {
            //create list to store the rooms 
            List<Activity> activities = new List<Activity>();

            foreach (DataRow dr in dataTable.Rows)
            {
                //store each room with the following fields from the database
                Activity activity = new Activity()
                {
                    Id = (int)(dr["Activity_id"]),
                    Name = (string)(dr["Activity_name"]),
                    StartDateTime = (DateTime)(dr["Activity_start_datetime"]),
                    EndDateTime = (DateTime)(dr["Activity_end_datetime"]),
                };
                activities.Add(activity);
            }
            return activities;
        }

        public void UpdateActivity(Activity activity)
        {
            //lower the stock by one for every order
            string query = $"UPDATE Activity SET Activity_name='{activity.Name}' WHERE Activity_id={activity.Id}";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query, sqlParameters);
        }
        public void AddToActivity(Activity activity)
        {
            // add new information to activity table
            string query = $"INSERT INTO Activity (Activity_name, Activity_start_datetime, Activity_end_datetime) VALUES ('{activity.Name}', '{activity.StartDateTime:yyyy-MM-dd HH:mm:ss}', '{activity.EndDateTime:yyyy-MM-dd HH:mm:s}');"; SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query, sqlParameters);
        }
        public void DeleteActivity(Activity activity)
        {
            string query = $"DELETE FROM Activity WHERE Activity_id={activity.Id}";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query, sqlParameters);
        }

    }
}
