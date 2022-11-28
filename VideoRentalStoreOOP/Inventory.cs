using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace VideoRentalStoreOOP
{
    //This class handles everything to do with the inventory of the rental shop.
    public class Inventory
    {
        public List<Film> Films;
        public Inventory()
        {
            Films = new List<Film>{

                new Film("The Godfather",Rental_Type.New_Release, 3, 0),
                new Film("How to Train Your Dragon", Rental_Type.New_Release, 3, 0),
                new Film("The Martian", Rental_Type.Regular_Rental, 3, 0),
                new Film("Doctor Strange", Rental_Type.Regular_Rental, 3, 0),
                new Film("The Dark Knight", Rental_Type.Old_Film, 3, 0),
                new Film("The Matrix", Rental_Type.Old_Film, 3, 30),
                new Film("Spider-man", Rental_Type.Regular_Rental, 30, 12),

                //Films that are not rented right now
                new Film("The Avengers", Rental_Type.Old_Film),
                new Film("Up", Rental_Type.New_Release),
                new Film("Aladdin", Rental_Type.New_Release),
                new Film("Alien", Rental_Type.New_Release),
                new Film("Alien 3", Rental_Type.New_Release),
                new Film("Aliens", Rental_Type.New_Release),
                new Film("Top Gun", Rental_Type.New_Release),
                new Film("Thor", Rental_Type.New_Release),
                new Film("Thor: Ragnarok", Rental_Type.New_Release),
                new Film("Toy Story", Rental_Type.New_Release),
                new Film("Venom", Rental_Type.New_Release),
                new Film("Transformers", Rental_Type.New_Release),
            };
        }
        #region Add, Remove, Change films
        public void AddFilm()
        {
            Console.WriteLine("What is the name of the film?");
            var name = Console.ReadLine();
            while (name == "")
            {
                Console.WriteLine("You must enter a name for the film. Type 'exit' to stop.");
                name = Console.ReadLine();
                if (name == "exit")
                {
                    Console.WriteLine("Action stopped.");
                    return;
                }
            }
            var rental_type = Rental_Type.Regular_Rental;
            Console.WriteLine("What film type is it?");
            switch (Menus.GetNumberFromUser("0. Exit\n1. New Release\n2. Regular rental\n3. Old film", max: 3, min:0))
            {
                case 1:
                    rental_type = Rental_Type.New_Release;
                    break;
                case 2:
                    rental_type = Rental_Type.Regular_Rental;
                    break;
                case 3:
                    rental_type = Rental_Type.Old_Film;
                    break;
                case 0:
                    Console.WriteLine("Action stopped.");
                    return;
            }
            Film film = new Film(name, rental_type, 0, 0);
            Films.Add(film);
            Console.WriteLine("Film added to inventory");
        }

        public void RemoveFilm()
        {
            if (Films.Count > 0)
            {
                Console.WriteLine("Which film do you wanna remove?");
            }
            while (true)
            {
                if (Films.Count < 1)
                {
                    Console.WriteLine("Our inventory is empty.");
                    return;
                }
                ShowAllFilms(true);
                int movieId = Menus.GetNumberFromUser("Type the number of the film to remove it. Type 0 to exit selection", max: Films.Count, min: 0) - 1;
                if (movieId == -1)
                {
                    break;
                }
                else
                {
                    Films.RemoveAt(movieId);
                    Console.WriteLine("Film removed");
                }

            }
        }
        public void ChangeFilm()
        {
            if (Films.Count > 0)
            {
                Console.WriteLine("Which film do you wanna change?");
            }
            while (true)
            {
                if (Films.Count < 1)
                {
                    Console.WriteLine("Our inventory is empty.");
                    return;
                }
                ShowAllFilms(true);
                int movieId = Menus.GetNumberFromUser("Type the number of the film to change it. Type 0 to exit selection", max: Films.Count, min: 0) - 1;
                if (movieId == -1)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("What film type is it?");
                    switch (Menus.GetNumberFromUser("0. Exit\n1. New Release\n2. Regular rental\n3. Old film", max: 3))
                    {
                        case 1:
                            Films[movieId].Rental_Type_ = Rental_Type.New_Release;
                            break;
                        case 2:
                            Films[movieId].Rental_Type_ = Rental_Type.Regular_Rental;
                            break;
                        case 3:
                            Films[movieId].Rental_Type_ = Rental_Type.Old_Film;
                            break;
                        case 0:
                            Console.WriteLine("Action stopped.");
                            return;
                    }
                    Console.WriteLine("Film type changed");

                }

            }

        }
        public void MakeFilmOverdue()
        {
            if (Films.Count > 0)
            {
                Console.WriteLine("Which film do you wanna change?");
            }
            while (true)
            {
                if (Films.Count < 1)
                {
                    Console.WriteLine("Our inventory is empty.");
                    return;
                }
                ShowAllFilms(true);
                int movieId = Menus.GetNumberFromUser("Type the number of the film to change it. Type 0 to exit selection", max: Films.Count, min: 0) - 1;
                if (movieId == -1)
                {
                    break;
                }
                else
                {
                    var days = Menus.GetNumberFromUser($"How many days is it overdue? Type in the number of days", max: Int32.MaxValue);
                    movieId = Films.IndexOf(Films.ElementAt(movieId));
                    Films[movieId].DaysOverdue = days;
                    Console.WriteLine("Success");
                }

            }

        }

        #endregion

        #region Rent, Rent with points
        public void RentFilms()
        {
            List<int> movieIds = new List<int>();
            List<Film> FilmsToRent;
            if (!NotRentedFilmsExist())
            {
                Console.WriteLine("Everything is rented. Try again tomorrow.");
                return;
            }
            Console.WriteLine("Which films do you want to rent? ");
            while (true)
            {
                if (!NotRentedFilmsExist() && movieIds.Count > 1)
                {
                    Console.WriteLine("You rented the last film");
                    break;
                }
                FilmsToRent = ShowAllNotRentedFilms(true);
                int movieId = Menus.GetNumberFromUser("Type the number of the film to rent it. Type 0 to exit selection", max: FilmsToRent.Count, min: 0) - 1;
                if (movieId == -1)
                {
                    break;
                }
                else
                {
                    var days = Menus.GetNumberFromUser("For how many days? Type in the number of days you want to rent this film", max: Int32.MaxValue);
                    movieId = Films.IndexOf(FilmsToRent.ElementAt(movieId));
                    Films[movieId].DaysRentedFor = days;
                    movieIds.Add(movieId);
                }
            }
            ShowReceipt(movieIds);
        }
        public void RentFilmUsingPoints()
        {
            if (Points.Bonus_points < 25)
            {
                Console.WriteLine("You do not have enough points to rent a film");
                return;
            }
            Console.WriteLine($"You have enough points for {Points.FreeRentDays} days for renting films.");
            if (!NotRentedFilmsExist())
            {
                Console.WriteLine("Unfortunately everything is rented. Try again tomorrow.");
                return;
            }
            List<int> movieIds = new List<int>();
            List<Film> FilmsToRent;
            var DaysUsed = 0;
            Console.WriteLine("Which films do you want to rent? ");
            while (DaysUsed < Points.FreeRentDays)
            {
                if (!NotRentedFilmsExist() && movieIds.Count == 0)
                {
                    Console.WriteLine("Everything is rented. Try again tomorrow.");
                    return;
                }
                else if (!NotRentedFilmsExist() && movieIds.Count > 1)
                {
                    Console.WriteLine("You rented the last film");
                    break;
                }
                FilmsToRent = ShowAllNotRentedFilms(true);
                int movieId = Menus.GetNumberFromUser("Type the number of the film to rent it. Type 0 to exit selection", max: FilmsToRent.Count, min: 0) - 1;
                if (movieId == -1)
                {
                    break;
                }
                else
                {
                    var days = Menus.GetNumberFromUser($"For how many days? Type in the number of days you want to rent this film (Maximum {Points.FreeRentDays - DaysUsed} days)", max: Points.FreeRentDays - DaysUsed);
                    movieId = Films.IndexOf(FilmsToRent.ElementAt(movieId));
                    Films[movieId].DaysRentedFor = days;
                    DaysUsed += days;
                    movieIds.Add(movieId);
                }
            }
            ShowReceipt(movieIds, boughtUsingPoints: true);
            Points.RemainingPoints();
        }


        #endregion

        #region ShowReceipt, ReturnOverdueFilms, ReturnFilms
        public void ReturnFilms()
        {
            List<Film> FilmsToReturn;
            Console.WriteLine("Which films do you want to return? ");
            while (true)
            {
                FilmsToReturn = ShowRented();
                if (FilmsToReturn.Count < 1)
                {
                    break;
                }
                int movieId = Menus.GetNumberFromUser("Type the number of the film to return it. Type 0 to exit selection", max: FilmsToReturn.Count, min: 0) - 1;
                if (movieId == -1)
                {
                    break;
                }
                else
                {
                    movieId = Films.IndexOf(FilmsToReturn.ElementAt(movieId));
                    Films[movieId].DaysRentedFor = 0;
                }
            }
            Console.WriteLine("\nFilms returned");

        }
        public void ReturnOverdueFilms()
        {
            List<Film> OverdueFilmsToReturn = GetAllOverdueFilms();

            if (OverdueFilmsToReturn.Count < 1)
            {
                Console.WriteLine("No overdue films to return");
                return;
            }

            Console.WriteLine("Your receipt:");
            int totalPrice = 0;

            foreach (var film in OverdueFilmsToReturn)
            {
                totalPrice += film.Overdue_Price;
                Console.WriteLine(film.Overdue_Info());
                film.DaysOverdue = 0;
                film.DaysRentedFor = 0;
            }

            Console.WriteLine($"Total late charge: {totalPrice} EUR");
        }
        public void ShowReceipt(List<int> movieIds, bool boughtUsingPoints = false)
        {
            if (movieIds.Count == 0)
            {
                return;
            }

            Console.WriteLine("Your receipt:");
            int totalPrice = 0;

            foreach (var movieId in movieIds)
            {
                totalPrice += Films[movieId].Price;
                if (boughtUsingPoints)
                {
                    Console.WriteLine(Films[movieId].General_Info() + $"Rented using {Films[movieId].DaysRentedFor * Points.PointsForAFreeRental} points");
                    Points.Bonus_points -= (Films[movieId].DaysRentedFor * 25);
                }
                else
                {
                    Points.AddPoints(Films[movieId]);
                    Console.WriteLine(Films[movieId].Rent_Info());
                }
            }

            if (!boughtUsingPoints)
            {
                Console.WriteLine($"Total price : {totalPrice} EUR");
            }
        }
        #endregion

        #region ShowAllFilms, ShowAllOverdueFilms, ShowAllNotRentedFilms
        public void ShowAllFilms(bool showId = false, bool showRented = false)
        {
            if (Films.Count < 1)
            {
                Console.WriteLine("Inventory is empty");
                return;
            }

            for (int i = 0; i < Films.Count; i++)
            {
                if (showId)
                {
                    Console.WriteLine($"{i + 1}: {Films[i].General_Info()}");
                }

                else if (Films[i].DaysRentedFor > 0 && showRented)
                {
                    Console.WriteLine($"{i + 1}: {Films[i].Rent_Info()}");
                }

                else if (Films[i].DaysRentedFor < 1)
                {
                    Console.WriteLine(Films[i].Rent_Info());
                }

                else
                {
                    Console.WriteLine(Films[i].General_Info());
                }
            }
        }
        public List<Film> GetAllOverdueFilms()
        {
            List<Film> overdueFilms = new List<Film>();

            for (int i = 0; i < Films.Count; i++)
            {
                if (Films[i].DaysOverdue > 0)
                {
                    overdueFilms.Add(Films[i]);
                }
            }

            return overdueFilms;
        }
        public List<Film> GetAllRentedFilms()
        {
            List<Film> rentedFilms = new List<Film>();

            for (int i = 0; i < Films.Count; i++)
            {
                if (Films[i].DaysRentedFor > 0 && Films[i].DaysOverdue < 1)
                {
                    rentedFilms.Add(Films[i]);
                }
            }

            return rentedFilms;
        }

        public List<Film> ShowAllNotRentedFilms(bool showId = false)
        {
            List<Film> availableFilms = new List<Film>();

            if (!NotRentedFilmsExist())
            {
                Console.WriteLine("Everything is rented. Try again tomorrow.");

                return availableFilms;
            }


            for (int i = 0; i < Films.Count; i++)
            {
                if (Films[i].DaysRentedFor < 1 && showId)
                {
                    availableFilms.Add(Films[i]);

                    Console.WriteLine($"{availableFilms.IndexOf(Films[i]) + 1}: {Films[i].Rent_Info()}");
                }

                else if (Films[i].DaysRentedFor < 1)
                {
                    Console.WriteLine(Films[i].Rent_Info());
                }
            }

            return availableFilms;
        }
        public List<Film> ShowRented()
        {
            List<Film> rentedFilms = new List<Film>();

            if (Films.Count < 1)
            {
                Console.WriteLine("Nothing to return.");
                return rentedFilms;
            }

            for (int i = 0; i < Films.Count; i++)
            {
                if (Films[i].DaysRentedFor > 0 && Films[i].DaysOverdue < 1)
                {
                    rentedFilms.Add(Films[i]);

                    Console.WriteLine($"{rentedFilms.IndexOf(Films[i]) + 1}: {Films[i].Rent_Info()}");
                }
            }

            return rentedFilms;
        }


        #endregion

        //Checks if there are any films to rent
        bool NotRentedFilmsExist()
        {
            for (int i = 0; i < Films.Count; i++)
            {
                if (Films[i].DaysRentedFor == 0)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
