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
    public class StudentService
    {
        StudentDao studentdb;

        public StudentService()
        {
            //create connection to database 
            studentdb = new StudentDao();
        }

        public List<Student> GetStudents()
        {
            //get all students from the database
            List<Student> students = studentdb.GetAllStudents();
            return students;
        }
    }
}
