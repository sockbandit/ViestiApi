using System.ComponentModel.DataAnnotations; // lisätty jotta voidaan käyttää tietojen validointia 

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

        [Required(ErrorMessage = "Etunimi vaaditaan")] // pakollinen etunimi sääntö
        [StringLength(20, ErrorMessage = "Etunimi saa olla enintään 20 merkkiä")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Sukunimi vaaditaan")] // pakollinen sukunimi sääntö
        [StringLength(20, ErrorMessage = "Sukunimi saa olla enintään 20 merkkiä")]
        public string LastName { get; set; } = "";
        public DateTime JoinedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }

    public class UserCreateDTO // DTO = Data Transfer Object, eli olio, joka siirtää dataa palvelimen ja asiakkaan välillä, mutta tässä tapauksessa se on tarkoitettu käyttäjän luomiseen, joten se sisältää myös salasanan
    {
        [Required(ErrorMessage = "Käyttäjätunnus vaaditaan")] // pakollinen käyttäjätunnus sääntö
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Käyttäjätunnus pitää olla vähintään 5 merkkiä ja enintään 20")] // vähintään 5 merkkiä enintään 20 
        public string Username { get; set; } = "";
        
        [Required(ErrorMessage = "Salasana vaaditaan")] // pakollinen salasana sääntö
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Salasanan pitää olla vähintään 8 merkkiä ja enintään 50")] // vähintään 8 merkkiä enintään 50
        public string Password { get; set; } = ""; 

        [Required(ErrorMessage = "Etunimi vaaditaan")] // pakollinen etunimi sääntö
        [StringLength(20, ErrorMessage = "Etunimi saa olla enintään 20 merkkiä")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Sukunimi vaaditaan")] // pakollinen sukunimi sääntö
        [StringLength(20, ErrorMessage = "Sukunimi saa olla enintään 20 merkkiä")]
        public string LastName { get; set; } = "";
    }


}

