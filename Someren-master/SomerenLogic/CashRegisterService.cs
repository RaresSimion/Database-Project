using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenDAL;
using SomerenModel;

namespace SomerenLogic
{
    public class CashRegisterService
    {
        CashRegisterDAO cashRegisterdb;

        public CashRegisterService()
        {
            //create connection to database
            cashRegisterdb = new CashRegisterDAO();
        }
        public void AddToRegister(int studentNumber, int drinkNumber, DateTime orderDate)
        {
            //add the order to the database
            cashRegisterdb.AddToRegister(studentNumber, drinkNumber, orderDate);
        }   
    }

}
