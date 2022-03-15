using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class RevenueReport
    {
        public int NumberOfDrinks { get; set; } //the number of drinks ordered in the time period selected
        public double Turnover { get; set; } //the total profit
        public int NumberOfCustomers { get; set; } //number of distinct customers in that period
    }
}
