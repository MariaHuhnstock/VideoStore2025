using System.Collections.Generic;
using VideoStore;

namespace VideoStore
{
    public class User
    {
        // Username + password for login
        public string Name { get; set; }
        public string Password { get; set; }

        // Flag determines whether user sees admin menu
        public bool IsAdmin { get; set; }

        // Movies this user has rented
        public List<Movie> BorrowedMovies { get; set; } = new List<Movie>();

        public User(string name, string password, bool isAdmin = false)
        {
            Name = name;
            Password = password;
            IsAdmin = isAdmin;
        }
    }
}
