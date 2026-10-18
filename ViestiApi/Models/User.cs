

namespace ViestiApi.Models
{
    public class User
    {
        public int Id { get; set; } // Tallentaa käyttäjän ID:n tietokantaan
        public string Username { get; set; } = ""; 
        public string PasswordHash { get; set; } = ""; 
        public string FirstName { get; set; } = ""; 
        public string LastName { get; set; } = ""; 
        public DateTime JoinedAt { get; set; } 

        //public DateTime LastLoginAt { get; set; } // vanha koodiversio, joka ei ollut nullable, joten käyttäjä ei välttämättä ollut kirjautunut sisään

        public DateTime? LastLoginAt { get; set; } // Tallentaa käyttäjän viimeisimmän kirjautumispäivämäärän tietokantaan, ?=nullable, koska käyttäjä ei välttämättä ole kirjautunut sisään

    }

    public class UserDTO // DTO = Data Transfer Object, eli olio, joka siirtää dataa palvelimen ja asiakkaan välillä
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime JoinedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }

    public class UserCreateDTO // DTO = Data Transfer Object, eli olio, joka siirtää dataa palvelimen ja asiakkaan välillä, mutta tässä tapauksessa se on tarkoitettu käyttäjän luomiseen, joten se sisältää myös salasanan
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = ""; // Tallentaa käyttäjän salasanan tietokantaan, mutta ei hash-muodossa
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }


}

