using ViestiApi.Models;

namespace ViestiApi.Repositories
{
    public class UserRepository : IUserRepository // toteuttaa IUserRepository-rajapinnan käyttäjien hallintaan
    {
        private readonly List<User> _users = new();  // väliaikainen "tietokanta"
        private int _nextId = 1;                     // seuraava vapaa id käyttäjälle

        public Task<List<User>> GetAllAsync() // palauttaa kaikki käyttäjät listana
        {
            return Task.FromResult(_users.ToList());
        }

        public Task<User?> GetByIdAsync(int id) // hakee käyttäjän Id:n perusteellaa
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(user);
        }

        public Task<User?> GetByUsernameAsync(string username) // hakee käyttäjän käyttäjä nimen perusteella
        {
            var user = _users.FirstOrDefault(u => u.Username == username);
            return Task.FromResult(user);
        }

        public Task AddAsync(User user) // lisää uuden käyttäjän listaan
        {
            user.Id = _nextId;  // annettaan uudelle käyttäjälle oma id
            _nextId++; // kasvatetaan seuraavaa vapaata id:tä
            _users.Add(user);
            return Task.CompletedTask; // palautetaaan 
        }

        public Task UpdateAsync(User user)
        {
            var index = _users.FindIndex(u => u.Id == user.Id);
            if (index != -1)
            {
                _users[index] = user;  // korvataan vanha käyttäjä uudella
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(User user) // poistetaan käyttäjä listasta
        {
            _users.Remove(user);
            return Task.CompletedTask;
        }
        
    }
}
