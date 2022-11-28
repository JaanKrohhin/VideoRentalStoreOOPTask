using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentalStoreOOP
{
    public class Film
    {
        public Film() { }
        public Film(string name, Rental_Type rental_Type_, int daysRentedFor, int daysOverdue)
        {
            Name = name;
            Rental_Type_ = rental_Type_;
            DaysRentedFor = daysRentedFor;
            DaysOverdue = daysOverdue;
        }
        public Film(string name, Rental_Type rental_Type_)
        {
            Name = name;
            Rental_Type_ = rental_Type_;
            DaysRentedFor = 0;
            DaysOverdue = 0;
        }
        //Film name
        public string Name { get; set; }
        //What type of rental is it: New release, regular rental or old film
        public Rental_Type Rental_Type_ { get; set; }
        //price per day of rent: premium price or basic price
        public Price_Type Price_Type_ {
            get
            {
                if (Rental_Type_ == Rental_Type.New_Release)
                {
                    return Price_Type.Premium_Price;
                }
                return Price_Type.Basic_Price;
            }
        }

        public int DaysRentedFor { get; set; }
        public int DaysOverdue { get; set; }
        public int Price 
        {
            get
            {
                return CalculatePrice();
            }
        }
        public int Overdue_Price
        {
            get
            {
                return (int)Price_Type_ * DaysOverdue;
            }
        }
        public string General_Info()
        {
                return $"{Name}({RentalTypeWithoutUnderscore(Rental_Type_)}) ";
        }
        public string Rent_Info()
        {
            if (DaysRentedFor > 0)
            {
                return $"{General_Info()}{DaysRentedFor} days {Price} EUR";
            }
            else
            {
                return $"{General_Info()}available";
            }
        }
        public string Overdue_Info()
        {
            return $"{General_Info()}{DaysOverdue} extra days {Overdue_Price} EUR";
        }

        //Removes the underscore to make text more beautiful/readable
        private string RentalTypeWithoutUnderscore(Rental_Type rental_Type)
        {
            return rental_Type.ToString().Replace('_', ' ');
        }

        #region Price calculation
        private int CalculatePrice()
        {
            switch (Rental_Type_)
            {
                case Rental_Type.New_Release:

                    return ((int)Price_Type.Premium_Price) * DaysRentedFor;

                case Rental_Type.Regular_Rental:

                    if (DaysRentedFor < 4)
                    {
                        return (int)Price_Type.Basic_Price;
                    }

                    else
                    {
                        return ((int)Price_Type.Basic_Price) + (int)Price_Type.Basic_Price * (DaysRentedFor - 3);
                    }

                case Rental_Type.Old_Film:

                    if (DaysRentedFor < 6)
                    {
                        return (int)Price_Type.Basic_Price;
                    }

                    else
                    {
                        return ((int)Price_Type.Basic_Price) + (int)Price_Type.Basic_Price * (DaysRentedFor - 5);
                    }
                    
                default:
                    return -1;
            }
        }
        #endregion
    }

}
