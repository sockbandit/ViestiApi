using System.Diagnostics.Contracts;

namespace ViestiApi.Models
{
    public class User
    {
        public int Id { get; set; } // Tallentaa käyttäjän ID:n tietokantaan
        public string Username { get; set; } = ""; // Tallentaa käyttäjänimen tietokantaan
        public string PasswordHash { get; set; } = ""; // Tallentaa salasanan hash-muodossa tietokantaan
        public string FirstName { get; set; } = ""; // Tallentaa käyttäjän etunimen tietokantaan
        public string LastName { get; set; } = ""; // Tallentaa käyttäjän sukunimen tietokantaa n
        public DateTime JoinedAt { get; set; } // Tallentaa käyttäjän liittymispäivämäärän tietokantaan
        public DateTime LastLoginAt { get; set; } // Tallentaa käyttäjän viimeisimmän kirjautumispäivämäärän tietokantaan :)


    }
}
