using ViestiApi.Models;
namespace ViestiApi.Services
{
    public interface IUserService // Käyttäjäpalvelun rajapinta
    {
        Task<List<UserDTO>> GetAllUsersAsync(); // sama logiikka kun = GetAllMessagesAsync mutta käyttäjille :)
        Task<UserDTO?> GetUserAsync(int id); // Hakee käyttäjän id:n perusteella
        Task<UserDTO?> GetUserByUsernameAsync(string username); // Hakee käyttäjän käyttäjänimen perusteella
        Task<UserDTO?> CreateUserAsync(UserCreateDTO user); // Luo uuden käyttäjän, mutta tarkistaa ensin, onko käyttäjänimi jo käytössä
        Task<bool> UpdateUserAsync(UserDTO user); // Päivittää olemassa olevan käyttäjän tiedot
        Task<bool> DeleteUserAsync(int id); // Poistaa käyttäjän id:n perusteella
    }
    // Rajapinta määrittelee käyttäjäpalvelun toiminnot, joita toteutetaan UserService luokassa
}
