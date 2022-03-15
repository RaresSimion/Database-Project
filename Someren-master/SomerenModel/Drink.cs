using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public  class Drink
    {
        public int Number { get; set; } // RoomNumber, e.g. 206
        public string Name { get; set; } // eg coca cola
        public bool IsAlcoholic{ get; set; } // Student = false, teacher = true
        public double Price { get; set; } // eg  100 euros
        public int Stock { get; set; }// eg yes if true no if false
    }
}
