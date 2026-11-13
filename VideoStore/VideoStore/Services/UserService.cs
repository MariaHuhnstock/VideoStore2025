using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VideoStore;

namespace VideoStore
{
    public static class UserService
    {
        private static List<User> users = new List<User>();
        private static string userFilePath = "data/users.txt";

        public static void Register(string name, string password)
        {
            if (users.Any(u => u.Name == name))
            {
                Console.WriteLine("User already exists!");
                return;
            }

            // Automatically make "admin" username an admin account
            bool isAdmin = (name.ToLower() == "admin");

            users.Add(new User(name, password, isAdmin));
            Console.WriteLine("User created!");
        }

        public static User Login(string name, string password)
        {
            return users.FirstOrDefault(u => u.Name == name && u.Password == password);
        }

        public static void SaveUsers()
        {
            Directory.CreateDirectory("data");
            using StreamWriter writer = new StreamWriter(userFilePath);

            // Format: name;password;isAdmin
            foreach (var user in users)
                writer.WriteLine($"{user.Name};{user.Password};{user.IsAdmin}");
        }

        public static void LoadUsers()
        {
            Directory.CreateDirectory("data");
            if (!File.Exists(userFilePath)) return;

            users.Clear();
            foreach (var line in File.ReadAllLines(userFilePath))
            {
                var parts = line.Split(';');
                users.Add(new User(parts[0], parts[1], bool.Parse(parts[2])));
            }
        }

        public static List<User> GetUsers() => users;
    }
}
