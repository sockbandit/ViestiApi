using Microsoft.EntityFrameworkCore;    
using ViestiApi.Models;

namespace ViestiApi.Repositories
{
    public class UserRepository : IUserRepository // toteuttaa IUserRepository-rajapinnan käyttäjien hallintaan
    {
        private readonly MessageServiceContext _context; // tietokantayhteys
        
        public UserRepository(MessageServiceContext context) // konstruktori, joka ottaa vastaan MessageServiceContextin
        {
            _context = context;
        }   

        public async Task<List<User>> GetAllAsync() // palauttaa kaikki käyttäjät listana
        {
            return await _context.Users.ToListAsync(); // hakee kaikki käyttäjät tietokannasta
        }

        public async Task<User?> GetByIdAsync(int id) // hakee käyttäjän id:n perusteella
        {
            return await _context.Users.FindAsync(id); // hakee käyttäjän tietokannasta id:n perusteella
        }

        public async Task<User?> GetByUsernameAsync(string username) // hakee käyttäjän käyttäjänimen perusteella
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username); // hakee käyttäjän tietokannasta käyttäjänimen perusteella
        }

        public async Task AddAsync(User user) // lisää uuden käyttäjän tietokantaan
        {
            _context.Users.Add(user); // lisää käyttäjän tietokantaan
            await _context.SaveChangesAsync(); // tallentaa muutokset tietokantaan
        }

        public async Task UpdateAsync(User user) // päivittää olemassa olevan käyttäjän tiedot tietokannassa
        {
            _context.Users.Update(user); // päivittää käyttäjän tietokantaan
            await _context.SaveChangesAsync(); // tallentaa muutokset tietokantaan
        }

        public async Task DeleteAsync(User user) // poistaa käyttäjän tietokannasta
        {
            _context.Users.Remove(user); // poistaa käyttäjän tietokannasta
            await _context.SaveChangesAsync(); // tallentaa muutokset tietokantaan
        }

    }
}
