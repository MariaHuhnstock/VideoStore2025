using System;
using System.IO;
using VideoStore;

namespace VideoStore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create store instance
            MovieStore store = new MovieStore();

            // Load saved data
            UserService.LoadUsers();
            store.LoadMovies();
            RatingService.LoadRatings(store);

            // If no movies exist yet, add some default movies
            if (store.Movies.Count == 0)
            {
                store.AddMovie(new Movie("Inception", "Sci-Fi"));
                store.AddMovie(new Movie("Shrek", "Animation"));
                store.AddMovie(new Movie("Gladiator", "Drama"));
                store.AddMovie(new Movie("The Matrix", "Sci-Fi"));
                store.AddMovie(new Movie("Interstellar", "Sci-Fi"));

                store.SaveMovies();
            }

            User loggedInUser = null;

            while (true)
            {
                // LOGIN MENU (loop until logged in)
                while (loggedInUser == null)
                {
                    Console.WriteLine("\n=== VIDEO STORE ===");
                    Console.WriteLine("(1) Register");
                    Console.WriteLine("(2) Login");
                    Console.WriteLine("(3) Exit");
                    Console.Write("> ");
                    string choice = Console.ReadLine();

                    Console.Write("Username: ");
                    string name = Console.ReadLine();
                    Console.Write("Password: ");
                    string password = Console.ReadLine();

                    if (choice == "1")
                        UserService.Register(name, password);
                    else if (choice == "2")
                        loggedInUser = UserService.Login(name, password);
                    else if (choice == "3")
                        return;
                }

                // ============================
                // ADMIN MENU
                // ============================
                if (loggedInUser.IsAdmin)
                {
                    Console.WriteLine("\n--- ADMIN MENU ---");
                    Console.WriteLine("(1) Show Movies");
                    Console.WriteLine("(2) Add Movie");
                    Console.WriteLine("(3) Remove Movie");
                    Console.WriteLine("(4) Show Top 5");
                    Console.WriteLine("(5) Logout");
                    Console.Write("> ");
                    string adminChoice = Console.ReadLine();

                    switch (adminChoice)
                    {
                        case "1":
                            store.ShowMovies(true);
                            break;

                        case "2":
                            Console.Write("Title: ");
                            string title = Console.ReadLine();
                            Console.Write("Genre: ");
                            string genre = Console.ReadLine();
                            store.AddMovie(new Movie(title, genre));
                            break;

                        case "3":
                            Console.Write("Title: ");
                            store.RemoveMovie(Console.ReadLine());
                            break;

                        case "4":
                            store.ShowTop5();
                            break;

                        case "5":
                            loggedInUser = null;
                            break;
                    }
                }
                // ============================
                // USER MENU
                // ============================
                else
                {
                    Console.WriteLine("\n--- USER MENU ---");
                    Console.WriteLine("(1) Show Movies");
                    Console.WriteLine("(2) Rent Movie");
                    Console.WriteLine("(3) Return Movie");
                    Console.WriteLine("(4) Search by Genre");
                    Console.WriteLine("(5) Logout");
                    Console.Write("> ");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            store.ShowMovies(true);
                            break;

                        case "2":
                            Console.Write("Title: ");
                            store.RentMovie(loggedInUser, Console.ReadLine());
                            break;

                        case "3":
                            Console.Write("Title: ");
                            store.ReturnMovie(loggedInUser, Console.ReadLine());
                            break;

                        case "4":
                            Console.Write("Genre: ");
                            store.SearchByGenre(Console.ReadLine());
                            break;

                        case "5":
                            loggedInUser = null;
                            break;
                    }
                }

                // Save changes after each action
                UserService.SaveUsers();
                store.SaveMovies();
            }
        }
    }
}
