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

        public async Task<List<UserDTO>> GetAllUsersAsync() // Palauttaa kaikki käyttäjät tietokannasta, toimii samalla tavalla kuin GetAllAsync-metodi UserRepositoryssa
        {
            var users = await _repository.GetAllAsync();
            return users.Select(u => UserToDTO(u)).ToList(); // Muuntaa User-oliot UserDTO-olioiksi ja palauttaa ne listana)
        }

        public async Task<UserDTO?> GetUserAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id); 
            if (user == null)
            {
                return null; // Jos käyttäjää ei löydy, palautetaan null
            }
            return UserToDTO(user); // Muuntaa User-olion UserDTO-olioksi ja palauttaa sen
        }

        public async Task<UserDTO?> GetUserByUsernameAsync(string username)
        {
            var user = await _repository.GetByUsernameAsync(username);
            if (user == null)
            {
                return null; // Jos käyttäjää ei löydy, palautetaan null
            }
            return UserToDTO(user); // Muuntaa User-olion UserDTO-olioksi ja palauttaa sen
        }

        public async Task<UserDTO?> CreateUserAsync(UserCreateDTO user)
        {
            var existing = await _repository.GetByUsernameAsync(user.Username); // Tarkistetaan, onko käyttäjätunnus jo olemassa tietokannassa
            if (existing != null) // Jos käyttäjätunnus on jo olemassa, palautetaan null
            {
                return null;
            }

            var newUser = new User // Luodaan uusi User-olio käyttäjän luomiseksi tietokantaan
            {
                Username = user.Username,
                PasswordHash = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                JoinedAt = DateTime.UtcNow
            };
            await _repository.AddAsync(newUser); 
            return UserToDTO(newUser); // Muuntaa User-olion UserDTO-olioksi ja palauttaa sen

        }

        public async Task<bool> UpdateUserAsync(UserDTO user) // Päivittää olemassa olevan käyttäjän tiedot tietokannassa 
        {
            var existing = await _repository.GetByIdAsync(user.Id);
            if (existing == null) // Jos käyttäjää ei löydy, palautetaan false eli käyttäjä ei löytynyt
            {
                return false;  
            }

            existing.FirstName = user.FirstName; 
            existing.LastName = user.LastName;

            await _repository.UpdateAsync(existing); // Päivitetään käyttäjän tiedot tietokantaan etunimen ja sukunimen osalt
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id) // Poistaa käyttäjän tietokannasta id:n perusteella
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            await _repository.DeleteAsync(user); 
            return true;
        }

        private UserDTO UserToDTO(User user) // Muuntaa User-olion UserDTO-olioksi, jotta voidaan palauttaa vain tarvittavat tiedot käyttäjästä
        {
            return new UserDTO // Luodaan uusi UserDTO-olio ja asetetaan sen ominaisuudet User-olion arvojen perusteella
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                JoinedAt = user.JoinedAt,
                LastLoginAt = user.LastLoginAt
            };
        }



    }
}
