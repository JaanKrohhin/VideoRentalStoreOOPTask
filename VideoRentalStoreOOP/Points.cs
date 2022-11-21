using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentalStoreOOP
{
    //Class that is responsible for customers bonus points
    public static class Points
    {
        public static int Bonus_points = 52;
        public static int FreeRentDays
        { 
            get
            {
                return Bonus_points / 25;
            } 
        }
        public static void AddPoints(Film film)
        {
            if (film.Rental_Type_ == Rental_Type.New_Release)
            {
                Bonus_points += 2;
            }
            else
            {
                Bonus_points++;
            }
        }
        public static void RemainingPoints()
        {
            Console.WriteLine("Bonus points: " + Bonus_points.ToString());
        }
    }
}
