using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenDAL;
using SomerenModel;

namespace SomerenLogic
{
    public class ActivityService
    {
        ActivityDAO activitydb;

        public ActivityService()
        {
            //create connection to database
            activitydb = new ActivityDAO();
        }
        public List<Activity> GetActivity()
        {
            //get all activity from the table in the database
            List<Activity> activities = activitydb.GetAllActivities();
            return activities;
        }

        public void UpdateActivity(string newName,int drinkNumber)
        {
            //update the activity stock in the database
            activitydb.UpdateActivity(newName,drinkNumber);
        }
        public void AddToActivity(int activityID,string activityName,  DateTime startDateTime, DateTime endDateTime)
        {
            //update the activity stock in the database
            activitydb.AddToActivity(activityID,activityName,startDateTime,endDateTime);
        }
        public void DeleteActivity( int activityID)
        {
            //update the activity stock in the database
            activitydb.DeleteActivity(activityID);
        }

    }
}
