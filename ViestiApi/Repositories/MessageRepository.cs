using Microsoft.EntityFrameworkCore;   
using ViestiApi.Models;

namespace ViestiApi.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MessageServiceContext _context; // tietokantayhteys
        
        public MessageRepository(MessageServiceContext context) // konstruktori, joka ottaa vastaan MessageServiceContextin
        {
            _context = context;
        }

        public async Task<List<Message>> GetAllAsync() // tässä metodissa haetaan kaikki viestit listana async menetelmällä
        {
            return await _context.Messages.ToListAsync(); // palautetaan kaikki viestit tietokannasta listana
        }

        public async Task<Message?> GetByIdAsync(long id) // tässä metodissa haetaan viesti id:n perusteella async menetelmällä
        {
            return await _context.Messages.FindAsync(id); // palautetaan viesti tietokannasta id:n perusteella
        }

        public async Task AddAsync(Message message) // tässä metodissa lisätään uusi viesti tietokantaan async menetelmällä
        {
            _context.Messages.Add(message); // lisätään viesti tietokantaan
            await _context.SaveChangesAsync(); // tallennetaan muutokset tietokantaan
        }

        public async Task UpdateAsync(Message message) // tässä metodissa päivitetään olemassa oleva viesti async menetelmällä
        {
            _context.Messages.Update(message); // päivitetään viesti tietokantaan
            await _context.SaveChangesAsync(); // tallennetaan muutokset tietokantaan
        }

        public async Task DeleteAsync(Message message) // tässä metodissa poistetaan viesti async menetelmällä
        {
            _context.Messages.Remove(message); // poistetaan viesti tietokannasta
            await _context.SaveChangesAsync(); // tallennetaan muutokset tietokantaan
        }
    }
}
