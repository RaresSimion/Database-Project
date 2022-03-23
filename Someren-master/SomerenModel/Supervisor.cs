using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Supervisor
    {
        public int TeacherId { get; set; } //the id of the teacher supervising
        public int ActivityId { get; set; } //the id of the activity that is being supervised
    }
}
