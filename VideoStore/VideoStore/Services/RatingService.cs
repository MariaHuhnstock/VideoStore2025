using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VideoStore;

namespace VideoStore
{
    public static class RatingService
    {
        private static string ratingFilePath = "data/ratings.txt";

        // Save a single rating to file
        public static void SaveRating(string title, int rating)
        {
            Directory.CreateDirectory("data");
            using StreamWriter writer = new StreamWriter(ratingFilePath, append: true);
            writer.WriteLine($"{title};{rating}");
        }

        // Load all ratings → calculate average per movie
        public static void LoadRatings(MovieStore store)
        {
            if (!File.Exists(ratingFilePath)) return;

            var groups = File.ReadAllLines(ratingFilePath)
                .Select(l => l.Split(';'))
                .GroupBy(x => x[0]);

            foreach (var group in groups)
            {
                var movie = store.Movies.FirstOrDefault(m => m.Title == group.Key);
                if (movie == null) continue;

                var ratings = group.Select(x => int.Parse(x[1])).ToList();
                movie.AverageRating = ratings.Average();
            }
        }
    }
}
