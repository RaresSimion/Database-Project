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

        public void UpdateActivity(Activity activity)
        {
            //update the activity stock in the database
            activitydb.UpdateActivity(activity);
        }
        public void AddToActivity(Activity activity)
        {
            //update the activity stock in the database
            activitydb.AddToActivity(activity);
        }
        public void DeleteActivity(Activity activity)
        {
            //update the activity stock in the database
            activitydb.DeleteActivity(activity);
        }

    }
}
