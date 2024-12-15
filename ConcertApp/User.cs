using System.Text.RegularExpressions;

namespace ConcertApp
{
    public class User
    {
        private static int _nextId = 0;
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        private string Password { get; set; }

        private static readonly List<User> users = new();

        public User(string firstName, string lastName, string password)
        {
            Id = _nextId++;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }

        public static void CreateAccount(User newUser)
        {
            if (IsValidAccount(newUser))
            {
                users.Add(new User(CreateValidName(newUser.FirstName),
                                   CreateValidName(newUser.LastName),
                                   newUser.Password));
            }
        }

        public static bool IsValidAccount(User newUser)
        {
            if (IsValidName(newUser.FirstName) && IsValidName(newUser.LastName) && IsValidPassword(newUser.Password))
                return true;

            return false;
        }


        public static bool TryLogin(User user)
        {
            bool userExists = users.Any(u =>
            u.FirstName == user.FirstName &&
            u.LastName == user.LastName &&
            u.Password == user.Password);

            return userExists;
        }

        private static string CreateValidName(string name)
        {
            return (char.ToUpper(name[0]) + name.Substring(1).ToLower()).Trim();
        }


        private static bool IsValidName(string name)
        {
            Regex regex = new("^[a-öA-Ö\\s-]+$");

            if (string.IsNullOrEmpty(name) || !regex.IsMatch(name))
                return false;
            
            return true;
        }

        private static bool IsValidPassword(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                if (password.Length < 6 || !password.Any(char.IsDigit) || !password.Any(char.IsUpper))
                    return false;
            }
            return true;
        }
    }
}
