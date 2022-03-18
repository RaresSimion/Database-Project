using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenModel;
using SomerenDAL;


namespace SomerenLogic
{
    public class DrinkService
    {
        DrinkDAO drinkdb;

        public DrinkService()
        {
            //create connection to database
            drinkdb = new DrinkDAO();
        }
        public List<Drink> GetDrinks()
        {
            //get all drinks from the table in the database
            List<Drink> drinks = drinkdb.GetAllDrinks();
            return drinks;
        }

        public void UpdateDrink(int drinkNumber)
        {
            drinkdb.UpdateDrink(drinkNumber);
        }
    }
}