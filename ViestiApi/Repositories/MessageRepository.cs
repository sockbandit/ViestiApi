using ViestiApi.Models;

namespace ViestiApi.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly List<Message> _messages = new();
        private long _nextId = 1;

        public Task<List<Message>> GetAllAsync() // tässä metodissa palautetaan kaikki viestit listana async menetelmällä
        {
            return Task.FromResult(_messages.ToList());
        }

        public Task<Message?> GetByIdAsync(long id) // tässä metodissa haetaan viesti id:n perusteella async menetelmällä
        {
            var message = _messages.FirstOrDefault(m => m.Id == id);
            return Task.FromResult(message);
        }

        public Task AddAsync(Message message) // tässä metodissa lisätään uusi viesti listaan async menetelmällä
        {
            message.Id = _nextId;
            _nextId++;
            _messages.Add(message);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Message message) // tässä metodissa päivitetään olemassa oleva viesti listassa async menetelmällä
        {
            var index = _messages.FindIndex(m => m.Id == message.Id); // etsitään viestin indeksi listasta idn perusteella
            if (index != -1)
            {
                _messages[index] = message; 
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Message message) // tässä metodissa poistetaan viesti listasta async menetelmällä
        {
            _messages.Remove(message);
            return Task.CompletedTask;
        }
    }
}
