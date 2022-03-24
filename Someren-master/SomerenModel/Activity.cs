using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Activity
    {
        public int Id { get; set; }// the id number 
        public string Name { get; set; }  // name of the activity eg hockey  
        public DateTime StartDateTime { get; set; }// start date and time of the activity
        public DateTime EndDateTime { get; set; }//end and time of the activity
    }
}
