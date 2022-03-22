using SomerenDAL;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenLogic
{
    public class TeacherService
    {
        TeacherDAO teacherdb;

        public TeacherService()
        {
            //create connection to database
            teacherdb = new TeacherDAO();
        }
        public List<Teacher> GetTeachers()
        {
            // get list of students from the database teachers
            List<Teacher> teachers = teacherdb.GetAllTeachers();
            return teachers;
        }

        public List<Teacher> GetSupervisors(int activityID)
        {
            List<Teacher> teachers = teacherdb.GetSupervisors(activityID);
            return teachers;
        }

        public List<Teacher> GetTeachersNotSupervising(int activityID)
        {
            List<Teacher> teachers = teacherdb.GetTeachersNotSupervising(activityID);
            return teachers;
        }
    }
}
