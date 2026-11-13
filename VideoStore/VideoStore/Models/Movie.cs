using VideoStore;

namespace VideoStore
{
    public class Movie
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; } = true;

        // Stats for rankings
        public int BorrowCount { get; set; } = 0;
        public double AverageRating { get; set; } = 0;

        public Movie(string title, string genre)
        {
            Title = title;
            Genre = genre;
        }
    }
}
