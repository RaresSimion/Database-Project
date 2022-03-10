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
    public class TeacherDAO : BaseDao
    {
        public List<Teacher> GetAllTeachers()
        {
            string query = "SELECT Teacher_number, Teacher_name, IsSupervisor FROM [Teacher]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Teacher> ReadTables(DataTable dataTable)
        {
            List<Teacher> teachers = new List<Teacher>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Teacher teacher = new Teacher()
                {
                    Number = (int)(dr["Teacher_number"]),
                    Name = (string)(dr["Teacher_name"]),
                    IsSupervisor = (bool)(dr["IsSupervisor"])
                };
                teachers.Add(teacher);
            }
            return teachers;
        }
    }
}
