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
            supervisordb = new SupervisorDAO();
        }

        public List<Teacher> GetSupervisors(int activityID)
        {
            List<Teacher> teachers = supervisordb.GetSupervisors(activityID);
            return teachers;
        }

        public List<Teacher> GetTeachersNotSupervising(int activityID)
        {
            List<Teacher> teachers = supervisordb.GetTeachersNotSupervising(activityID);
            return teachers;
        }
        
        public void AddSupervisor(int teacherID, int activityID)
        {
            supervisordb.AddSupervisor(teacherID, activityID);
        }
    }
}
