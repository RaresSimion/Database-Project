using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Teacher
    {
        public string Name { get; set; } //Teacher name
        public int Number { get; set; } // LecturerNumber, e.g. 47198

        //public string Subject { get; set; }
        //public int RoomNumber { get; set; }
        public bool IsSupervisor { get; set; } //If the teacher is a supervisor IsSupervisor=true, else IsSupervisor=false
    }
}
