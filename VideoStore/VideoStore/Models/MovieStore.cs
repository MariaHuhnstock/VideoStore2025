using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VideoStore;

namespace VideoStore
{
    public class MovieStore
    {
        public List<Movie> Movies { get; set; } = new List<Movie>();

        string movieFilePath = "data/movies.txt";

        // Add a new movie to the library
        public void AddMovie(Movie movie)
        {
            Movies.Add(movie);
        }

        // Remove movie from library
        public void RemoveMovie(string title)
        {
            var movie = Movies.FirstOrDefault(m => m.Title == title);
            if (movie == null)
            {
                Console.WriteLine("Movie not found!");
                return;
            }

            Movies.Remove(movie);
            Console.WriteLine("Movie removed!");
        }

        // List movies with availability and stats
        public void ShowMovies(bool showRatings = false)
        {
            Console.WriteLine("\n--- MOVIES ---");
            foreach (var movie in Movies)
            {
                Console.WriteLine($"{movie.Title} ({movie.Genre}) | " +
                    $"{(movie.IsAvailable ? "Available" : "Rented")} | " +
                    $"Borrowed: {movie.BorrowCount}x | Rating: {movie.AverageRating:F1}");
            }
        }

        // Top 5 ranking based on borrow count
        public void ShowTop5()
        {
            Console.WriteLine("\n--- TOP 5 MOVIES ---");
            foreach (var m in Movies.OrderByDescending(m => m.BorrowCount).Take(5))
                Console.WriteLine($"{m.Title} | {m.BorrowCount} borrows | ⭐ {m.AverageRating:F1}");
        }

        // Filter movies by genre
        public void SearchByGenre(string genre)
        {
            Console.WriteLine($"\n--- MOVIES IN GENRE: {genre} ---");
            foreach (var m in Movies.Where(m => m.Genre.ToLower() == genre.ToLower()))
                Console.WriteLine($"{m.Title} | ⭐ {m.AverageRating:F1}");
        }

        // Rent a movie to a user
        public void RentMovie(User user, string title)
        {
            var movie = Movies.FirstOrDefault(m => m.Title == title);

            if (movie == null)
            {
                Console.WriteLine("Movie not found!");
                return;
            }
            if (!movie.IsAvailable)
            {
                Console.WriteLine("This movie is already rented!");
                return;
            }

            movie.IsAvailable = false;
            movie.BorrowCount++;
            user.BorrowedMovies.Add(movie);

            Console.WriteLine($"You rented '{movie.Title}'.");
        }

        // Return and rate a movie
        public void ReturnMovie(User user, string title)
        {
            var movie = user.BorrowedMovies.FirstOrDefault(m => m.Title == title);

            if (movie == null)
            {
                Console.WriteLine("You did not rent this movie!");
                return;
            }

            movie.IsAvailable = true;
            user.BorrowedMovies.Remove(movie);

            Console.Write("Rate this movie (1–5): ");
            int rating = int.Parse(Console.ReadLine());
            RatingService.SaveRating(movie.Title, rating);

            RatingService.LoadRatings(this);

            Console.WriteLine($"Returned and rated: {movie.Title}");
        }

        // Save all movies to file
        public void SaveMovies()
        {
            Directory.CreateDirectory("data");
            using StreamWriter writer = new StreamWriter(movieFilePath);

            foreach (var m in Movies)
                writer.WriteLine($"{m.Title};{m.Genre};{m.IsAvailable};{m.BorrowCount};{m.AverageRating}");
        }

        // Load movies from file
        public void LoadMovies()
        {
            Directory.CreateDirectory("data");
            if (!File.Exists(movieFilePath)) return;

            Movies.Clear();
            foreach (var line in File.ReadAllLines(movieFilePath))
            {
                var parts = line.Split(';');
                Movie m = new Movie(parts[0], parts[1]);
                m.IsAvailable = bool.Parse(parts[2]);
                m.BorrowCount = int.Parse(parts[3]);
                m.AverageRating = double.Parse(parts[4]);
                Movies.Add(m);
            }
        }
    }
}
