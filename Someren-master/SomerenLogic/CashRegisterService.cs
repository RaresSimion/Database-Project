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
        public void AddRegister(int studentNumber, int drinkNumber)
        {
            cashRegisterdb.AddRegister(studentNumber, drinkNumber);
        }
       
    }

}
