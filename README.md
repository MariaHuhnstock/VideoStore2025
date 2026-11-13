
# Video Store

A C# console application developed as a learning project during my 20-week internship at Buss-IT-Labs.

## Features

- Register and log in with local user accounts
- Browse movies, their genres, availability, rental counts, and average ratings
- Rent and return movies
- Rate a movie when returning it
- Search movies by genre
- View the five most frequently rented movies (admin menu)
- Add and remove movies (admin menu)
- Save movie records, user accounts, and ratings in local text files
- Initialize the catalog with five sample movies when no saved movies exist

## Technologies

- C# / .NET 10
- Console application
- Object-oriented programming, collections, and LINQ
- Local file I/O

## Project Structure

```text
VideoStore.slnx
VideoStore/
  VideoStore.csproj
  Program.cs
  Models/
    Movie.cs
    MovieStore.cs
    User.cs
  Services/
    RatingService.cs
    UserService.cs
```

## Run

Requires the .NET 10 SDK. Open `VideoStore.slnx` in a compatible Visual Studio version, or from the repository root run:

```bash
dotnet run --project VideoStore/VideoStore.csproj
```

The application creates a `data` directory relative to its current working directory. Its text files are excluded from version control.

## Learning Focus

Practice working with classes and objects, console menus, user input, lists, LINQ queries, and saving/loading local data.

## Known Limitations

This is an educational prototype, not a production-ready video rental system.

- User passwords are stored as plain text. Use only disposable test credentials.
- Registering with the username `admin` automatically grants admin privileges.
- The rental state is stored with each movie, but the list of movies borrowed by each user is not persisted. After restarting, an unavailable movie may no longer be returnable by the original borrower.
- Ratings are parsed as integers without checking the allowed range (1–5); invalid input can terminate the program.
- The login menu asks for a username and password even when choosing Exit.
- Text file records use semicolon delimiters without escaping or robust validation.

## Possible Improvements

Add input validation and error handling, persist user rental records, restrict administrator creation, hash passwords, and add automated tests.

---

Original internship learning project. Source code is preserved as written; this repository documents the original implementation.
