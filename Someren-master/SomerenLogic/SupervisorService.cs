using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenModel;
using SomerenDAL;

namespace SomerenLogic
{
    public class SupervisorService
    {
        SupervisorDAO supervisordb;

        public SupervisorService()
        {
            //create connection to database
            supervisordb = new SupervisorDAO();
        }

        //get all supervisors for the selected activity from the table
        public List<Teacher> GetSupervisors(int activityID)
        {
            List<Teacher> teachers = supervisordb.GetSupervisors(activityID);
            return teachers;
        }
        
        //get all teachers who dont supervise the selected activity from the table
        public List<Teacher> GetTeachersNotSupervising(int activityID)
        {
            List<Teacher> teachers = supervisordb.GetTeachersNotSupervising(activityID);
            return teachers;
        }
        
        //adding a supervisor to the selected activity
        public void AddSupervisor(int teacherID, int activityID)
        {
            supervisordb.AddSupervisor(teacherID, activityID);
        }

        //removing a supervisor from the selected activity
        public void RemoveSupervisor(int teacherID, int activityID)
        {
            supervisordb.RemoveSupervisor(teacherID, activityID);
        }
    }
}
