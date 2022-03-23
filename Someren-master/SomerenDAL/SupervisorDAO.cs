using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenModel;

namespace SomerenDAL
{
    public class SupervisorDAO : BaseDao
    {
        public List<Teacher> GetSupervisors(int activityID)
        {
            string query = $"SELECT Teacher_number, Teacher_name, IsSupervisor FROM [Teacher] WHERE Teacher_number IN (SELECT Lecturer_id FROM Activity_supervisor WHERE Activity_id={activityID})";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        public List<Teacher> GetTeachersNotSupervising(int activityID)
        {
            string query = $"SELECT Teacher_number, Teacher_name, IsSupervisor FROM [Teacher] WHERE Teacher_number NOT IN (SELECT Lecturer_id FROM Activity_supervisor WHERE Activity_id={activityID})";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        public void AddSupervisor(int teacherID, int activityID)
        {
            string query = $"INSERT INTO Activity_supervisor VALUES ({teacherID}, {activityID});";
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
