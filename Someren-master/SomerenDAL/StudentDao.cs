using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using SomerenModel;

namespace SomerenDAL
{
    public class StudentDao : BaseDao
    {      
        public List<Student> GetAllStudents()
        {
            // select columns from database
            string query = "SELECT Student_number, Student_name, Room_number FROM [Student]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Student> ReadTables(DataTable dataTable)
        {
            // create list to store students
            List<Student> students = new List<Student>();

            foreach (DataRow dr in dataTable.Rows)
            {
                //store these fields in each students from the columns in the database
                Student student = new Student()
                {
                    Number = (int)(dr["Student_number"]),
                    Name = (string)(dr["Student_name"]),
                    RoomNumber = (int)dr["Room_number"],
                };
                students.Add(student);
            }
            return students;
        }
    }
}
