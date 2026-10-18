using ViestiApi.Models;
using ViestiApi.Repositories; 
namespace ViestiApi.Services
{
    public class UserService : IUserService // Toteuttaa IUserService rajapinnan, joka määrittelee käyttäjäpalvelun toiminnot
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)  // Konstruktorissa injektoidaan IUserRepository riippuvuus, jotta UserService voi käyttää sitä käyttäjien hallintaan
        {
            _repository = repository;
        }

        public async Task<List<User>> GetAllUsersAsync() // Palauttaa kaikki käyttäjät tietokannasta, toimii samalla tavalla kuin GetAllAsync-metodi UserRepositoryssa
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _repository.GetByUsernameAsync(username);
        }

        public async Task<User?> CreateUserAsync(User user)
        {
            var existing = await _repository.GetByUsernameAsync(user.Username); // Tarkistetaan, onko käyttäjätunnus jo olemassa tietokannassa
            if (existing != null) // Jos käyttäjätunnus on jo olemassa, palautetaan null
            {
                return null;
            }

            user.JoinedAt = DateTime.UtcNow; // Asetetaan käyttäjän liittymisaika nykyhetkeen UTC aikavyöhykkeellä
            await _repository.AddAsync(user); // Lisätään uusi käyttäjä tietokantaan
            return user;
        }

        public async Task<bool> UpdateUserAsync(User user) // Päivittää olemassa olevan käyttäjän tiedot tietokannassa
        {
            var existing = await _repository.GetByIdAsync(user.Id);
            if (existing == null) // Jos käyttäjää ei löydy, palautetaan false eli käyttäjä ei löytynyt
            {
                return false;  // käyttäjää ei löytynyt
            }

            existing.FirstName = user.FirstName; 
            existing.LastName = user.LastName;

            await _repository.UpdateAsync(existing); // Päivitetään käyttäjän tiedot tietokantaan etunimen ja sukunimen osalta
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id) // Poistaa käyttäjän tietokannasta id:n perusteella
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            await _repository.DeleteAsync(user); // Poistetaan käyttäjä tietokannasta
            return true;
        }



    }
}
