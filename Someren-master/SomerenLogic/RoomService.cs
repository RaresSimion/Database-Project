using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenModel;
using SomerenDAL;



namespace SomerenLogic
{
    public class RoomService
    {
        RoomDAO roomdb;

        public RoomService()
        {
            //create connection to database
            roomdb = new RoomDAO();
        }

        public List<Room> GetRooms()
        {
            //get all roooms from the table in the database
            List<Room> rooms = roomdb.GetAllRooms();
            return rooms;
        }
    }
}

