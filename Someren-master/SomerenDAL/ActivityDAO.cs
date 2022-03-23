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
            string query = "SELECT ActivityID,Activity_name,ActicityStartDateTime,ActivityEndDateTime FROM Activity";
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
                    Id = (int)(dr["ActivityID"]),
                    Name = (string)(dr["Activity_name"]),
                    StartDateTime = (DateTime)(dr["ActicityStartDateTime"]),
                    EndDateTime = (DateTime)(dr["ActivityEndDateTime"]),
                };
                activities.Add(activity);
            }
            return activities;
        }

        public void UpdateActivity(string newName,int activityID, DateTime startDateTime, DateTime endDateTime)
        {
            //lower the stock by one for every order
            string query = $"UPDATE Activity SET Activity_name= '{newName}',ActicityStartDateTime ={startDateTime} WHERE ActivityID={activityID}";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query, sqlParameters);
        }
        public void AddToActivity(int activityID,string activityName, DateTime startDateTime, DateTime endDateTime)
        {
            // add new information to activity table
            string query = $"INSERT INTO Activity (ActivityID,Activity_name,ActicityStartDateTime,ActivityEndDateTime) VALUES ({activityID},'{activityName}', '{startDateTime:yyyy - MM - dd HH-mm-ss}', '{endDateTime:yyyy-MM-dd HH-mm-s}')";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query, sqlParameters);
        }
        public void DeleteActivity(int activityID)
        {
            string query = $"DELETE from Activity WHERE ActivityID={activityID}";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query, sqlParameters);
        }

    }
}
