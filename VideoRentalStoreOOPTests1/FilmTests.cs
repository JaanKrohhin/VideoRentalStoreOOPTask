using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoRentalStoreOOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentalStoreOOP.Tests
{
    [TestClass()]
    public class FilmTests
    {
        private string RandomString()
        {
            Random random = new Random();
            int stringlen = random.Next(4, 20);
            int randValue;
            string str = "";
            char letter;
            for (int j = 0; j < stringlen; j++)
            {

                randValue = random.Next(0, 26);

                letter = Convert.ToChar(randValue + 65);
                str = str + letter;
            }
            return str;

        }

        [TestMethod()]
        public void PriceTest()
        {
            Random random = new Random();
            for (int i = 0; i < 10000; i++)
            {
                var rental_type = (Rental_Type)random.Next(3);
                var days = random.Next(10,1001);
                var price_plan = rental_type == Rental_Type.New_Release ? Price_Type.Premium_Price : Price_Type.Basic_Price;
                Film film = new Film("Test", rental_type, days, 0);

                System.Diagnostics.Trace.WriteLine(rental_type);
                System.Diagnostics.Trace.WriteLine(days);
                System.Diagnostics.Trace.WriteLine(price_plan);
                System.Diagnostics.Trace.WriteLine(film.Price);


                if (rental_type ==  Rental_Type.New_Release)
                {
                    Assert.AreEqual(film.Price, (int)price_plan * days);
                }
                else if (rental_type == Rental_Type.Regular_Rental)
                {
                    Assert.AreEqual(film.Price, (int)price_plan + (int)price_plan * (days - 3));
                }
                else
                {
                    Assert.AreEqual(film.Price, (int)price_plan + (int)price_plan * (days - 5));
                }
            }
        }


        [TestMethod()]
        public void General_InfoTest()
        {
            Random random = new Random();
            for (int i = 0; i < 10000; i++)
            {
                var rental_type = (Rental_Type)random.Next(3);
                var name = RandomString();
                Film film = new Film(name, rental_type, 0, 0);
                System.Diagnostics.Trace.WriteLine(name);
                System.Diagnostics.Trace.WriteLine(film.Name);
                Assert.AreEqual(film.General_Info(), name+"("+rental_type.ToString()+") ");

            }
        }
        [TestMethod()]
        public void Rent_InfoTest()
        {
            Random random = new Random();
            for (int i = 0; i < 10000; i++)
            {
                var rental_type = (Rental_Type)random.Next(3);
                var name = RandomString();
                var days = random.Next(100,10001);
                var price_plan = rental_type == Rental_Type.New_Release ? Price_Type.Premium_Price : Price_Type.Basic_Price;
                Film film = new Film(name, rental_type, days, 0);
                System.Diagnostics.Trace.WriteLine(name);
                System.Diagnostics.Trace.WriteLine(film.Name);
                System.Diagnostics.Trace.WriteLine(rental_type);
                System.Diagnostics.Trace.WriteLine(days);
                System.Diagnostics.Trace.WriteLine(film.Rent_Info());
                System.Diagnostics.Trace.WriteLine("");
                int price = 0;
                if (rental_type == Rental_Type.New_Release)
                {
                    price = (int)price_plan * days;
                }
                else if (rental_type == Rental_Type.Regular_Rental)
                {
                    price = (int)price_plan + (int)price_plan * (days - 3);
                }
                else
                {
                    price = (int)price_plan + (int)price_plan * (days - 5);
                }
                string expectedString = name + "(" + rental_type.ToString() + ") " + days.ToString() + " days " + price.ToString() + " EUR";
                System.Diagnostics.Trace.WriteLine(expectedString);
                Assert.AreEqual(film.Rent_Info(), expectedString);
            }
        }

        [TestMethod()]
        public void Overdue_InfoTest()
        {
            Random random = new Random();
            for (int i = 0; i < 10000; i++)
            {
                var rental_type = (Rental_Type)random.Next(3);
                var name = RandomString();
                var daysOver = random.Next(10001);
                var price_plan = rental_type == Rental_Type.New_Release ? Price_Type.Premium_Price : Price_Type.Basic_Price;
                Film film = new Film(name, rental_type, 1, daysOver);
                System.Diagnostics.Trace.WriteLine(name);
                System.Diagnostics.Trace.WriteLine(price_plan);
                System.Diagnostics.Trace.WriteLine(film.Name);
                System.Diagnostics.Trace.WriteLine(film.DaysOverdue);
                System.Diagnostics.Trace.WriteLine(film.Overdue_Price);
                System.Diagnostics.Trace.WriteLine("");
                Assert.AreEqual(film.Overdue_Info(), name + "(" + rental_type.ToString() + ") "+daysOver.ToString()+ " extra days "+(int)price_plan * daysOver +" EUR");

            }
        }


    }
}