using ViestiApi.Models;
namespace ViestiApi.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(); // Hakee kaikki käyttäjät tietokannasta asynkronisesti
        Task<User?> GetByIdAsync(int id); // Hakee käyttäjän tietokannasta ID:n perusteella
        Task<User?> GetByUsernameAsync(string username); // Hakee käyttäjän tietokannasta käyttäjänimen perusteella
        Task AddAsync(User user); // Lisää uusi käyttäjä tietokantaan
        Task UpdateAsync(User user); // Päivittää olemassa olevan käyttäjän tiedot tietokannassa
        Task DeleteAsync(User user); // Poistaa käyttäjän tietokannasta asynkronisesti
    }
}
