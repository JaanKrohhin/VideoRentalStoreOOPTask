using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentalStoreOOP
{
    //This class handles all the menus inside the console for the user
    public static class Menus
    {
        static Inventory storeInventory = new Inventory();
        public static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine();
                switch (Menus.GetNumberFromUser("\n0. Exit\n1. Show shop inventory\n2. Rent a film\n3. Return films\n4. Show bonus points\n5. Manage inventory", min: 0, max: 5))
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        ShowInventoryMenu();
                        break;
                    case 2:
                        RentMenu();
                        break;
                    case 3:
                        ReturnMenu();
                        break;
                    case 4:
                        Points.RemainingPoints();
                        Console.ReadLine();
                        break;
                    case 5:
                        ManageInventoryMenu();
                        break;
                }
                Console.Clear();
            }
        }

        private static void ManageInventoryMenu()
        {
            while (true)
            {
                switch (Menus.GetNumberFromUser("\n0. Back\n1. Add a film\n2. Remove film\n3. Change film type\n4. Make film overdue", min: 0, max: 4))
                {
                    case 0:
                        return;
                    case 1:
                        storeInventory.AddFilm();
                        break;
                    case 2:
                        storeInventory.RemoveFilm();
                        break;
                    case 3:
                        storeInventory.ChangeFilm();
                        break;
                    case 4:
                        storeInventory.MakeFilmOverdue();
                        break;
                }
            }
        }

        private static void ShowInventoryMenu()
        {
            while (true)
            {
                switch (Menus.GetNumberFromUser("\n0. Back\n1. Show all films\n2. Show all available for rent films", min: 0, max: 2))
                {
                    case 0:
                        return;
                    case 1:
                        storeInventory.ShowAllFilms();
                        break;
                    case 2:
                        storeInventory.ShowAllNotRentedFilms();
                        break;
                }
            }
        }

        private static void RentMenu()
        {
            while (true)
            {
                switch (GetNumberFromUser("\n0. Back\n1. Rent normally\n2. Rent using bonus points\n3. Show bonus points", min: 0, max: 3))
                {
                    case 0:
                        return;
                    case 1:
                        storeInventory.RentFilms();
                        break;
                    case 2:
                        storeInventory.RentFilmUsingPoints();
                        break;
                    case 3:
                        Points.RemainingPoints();
                        break;
                }
            }
        }
        private static void ReturnMenu()
        {
            while (true)
            {
                switch (GetNumberFromUser("\n0. Back\n1. Return films\n2. Return overdue films", min: 0, max: 2))
                {
                    case 0:
                        return;
                    case 1:
                        storeInventory.ReturnFilms();
                        break;
                    case 2:
                        storeInventory.ReturnOverdueFilms();
                        break;
                }
            }
        }
        public static int GetNumberFromUser(string displayMessage, int min = 1, int max = 3)
        {
            int selectedNumber = min - 1;
            while (selectedNumber < min || selectedNumber > max)
            {
                try
                {
                    Console.WriteLine(displayMessage);
                    selectedNumber = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Not a number");
                }
                Console.WriteLine("\n");
            }
            return selectedNumber;
        }


    }
}
