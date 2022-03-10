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
    public class RoomDAO : BaseDao
    {
        public List<Room> GetAllRooms()
        {
            // select columns from database
            string query = "SELECT Room_number,Room_capacity,Room_type FROM [Room]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Room> ReadTables(DataTable dataTable)
        {
            //create list to store the rooms 
            List<Room> rooms = new List<Room>();

            foreach (DataRow dr in dataTable.Rows)
            {
                //store each room with the following fields from the database
                Room room = new Room()
                {
                    Number = (int)(dr["Room_number"]),
                    Capacity = (int)(dr["Room_capacity"]),
                    Type = (bool)(dr["Room_type"])
                };
                rooms.Add(room);
            }
            return rooms;
        }
    }
}
