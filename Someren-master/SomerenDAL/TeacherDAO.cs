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
        //getting the teachers from the db
        public List<Teacher> GetAllTeachers()
        {
            string query = "SELECT Teacher_number, Teacher_name, IsSupervisor FROM [Teacher]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        //modifying the IsSupervisor column to true if the teacher is a supervisor
        public void AddIsSupervisor()
        {
            string query = $"UPDATE Teacher SET IsSupervisor = 'true' WHERE Teacher_number IN (SELECT DISTINCT Lecturer_id FROM Activity_supervisor);";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query, sqlParameters);
        }

        //modifying the IsSupervisor column to false if the teacher is no longer a supervisor
        public void RemoveIsSupervisor()
        {
            string query = $"UPDATE Teacher SET IsSupervisor = 'false' WHERE Teacher_number NOT IN (SELECT DISTINCT Lecturer_id FROM Activity_supervisor);";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query, sqlParameters);
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
