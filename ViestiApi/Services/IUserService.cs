using ViestiApi.Models;
namespace ViestiApi.Services
{
    public interface IUserService // Käyttäjäpalvelun rajapinta
    {
        Task<List<User>> GetAllUsersAsync(); // sama logiikka kun = GetAllMessagesAsync mutta käyttäjille :)
        Task<User?> GetUserAsync(int id); // Hakee käyttäjän id:n perusteella
        Task<User?> GetUserByUsernameAsync(string username); // Hakee käyttäjän käyttäjänimen perusteella
        Task<User?> CreateUserAsync(User user); // Luo uuden käyttäjän, mutta tarkistaa ensin, onko käyttäjänimi jo käytössä
        Task<bool> UpdateUserAsync(User user); // Päivittää olemassa olevan käyttäjän tiedot
        Task<bool> DeleteUserAsync(int id); // Poistaa käyttäjän id:n perusteella
    }
    // Rajapinta määrittelee käyttäjäpalvelun toiminnot, joita toteutetaan UserService luokassa
}
