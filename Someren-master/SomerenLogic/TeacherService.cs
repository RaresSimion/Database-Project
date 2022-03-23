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

        public void AddIsSupervisor()
        {
            teacherdb.AddIsSupervisor();
        }

        public void RemoveIsSupervisor()
        {
            teacherdb.RemoveIsSupervisor();
        }
    }
}
