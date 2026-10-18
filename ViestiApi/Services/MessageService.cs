using ViestiApi.Models;
using ViestiApi.Repositories;

namespace ViestiApi.Services

{
    public class MessageService : IMessageService // viestien hallintaan käytettävä service
    {
        private readonly IMessageRepository _repository; // viestien hallintaan käytettävä repository

        public MessageService(IMessageRepository repository) // konstruktorissa injektoidaan repository
        {
            _repository = repository;
        }

        public async Task<List<Message>> GetPublicMessagesAsync() // palauttaa kaikki julkiset viestit (joilla ei ole vastaanottajaa)
        {
            var messages = await _repository.GetAllAsync();
            return messages.Where(m => m.ReceiverId == null).ToList();
        }
        public async Task<List<Message>> GetMessagesForUserAsync(int userId) // palauttaa kaikki viestit jotka käyttäjä voi nähdä
        {
            var messages = await _repository.GetAllAsync(); // haetaan kaikki viestit tietokannasta
            return messages
                .Where(m => m.ReceiverId == null // käyttäjä voi nähdä viestin jos se on julkinen (ei vastaanottajaa)
                         || m.SenderId == userId // tai jos käyttäjä on viestin lähettäjä
                         || m.ReceiverId == userId) // tai jos käyttäjä on viestin vastaanottaja
                .ToList(); // palautetaan viestit listana

            // | merkki kopioitavaksi
        }

        public async Task<Message?> GetMessageAsync(int id, int userId) // palauttaa yksittäisen viestin id:n perusteella, mutta tarkistaa myös, että käyttäjä on oikeutettu näkemään viestin
        {
            var message = await _repository.GetByIdAsync(id);
            if (message == null)
            {
                return null;
            }

            bool canSee = message.ReceiverId == null //cansee tarkoittaa että käyttäjä voi nähdä viestin jos se on julkinen (ei vastaanottajaa)
                       || message.SenderId == userId //tai jos käyttäjä on viestin lähettäjä
                       || message.ReceiverId == userId; //tai jos käyttäjä on viestin vastaanottaja :)

            return canSee ? message : null; // palautetaan viesti vain, jos käyttäjä saa nähdä sen eli muut ei voi nähdäsitä
        }

        public async Task<Message?> SendMessageAsync(Message message) // viestin lähettäminen 
        {
            if (message.PreviousMessageId != null) // tarkistetaan, onko viesti vastaus johonkin vertaamalla viestin PreviousMessageId:hen, jos se on null, niin viesti ei ole vastaus
            {
                var previous = await _repository.GetByIdAsync(message.PreviousMessageId.Value);
                if (previous == null)
                {
                    return null;
                }
            }

            await _repository.AddAsync(message);
            return message;
        }

        public async Task<bool> UpdateMessageAsync(Message message, int userId)
        {
            var existing = await _repository.GetByIdAsync(message.Id);

            if (existing == null || existing.SenderId != userId) // vain oman viestin saa muokata
            {
                return false; // palautetaan false, jos viestiä ei löydy tai käyttäjä ei ole sen lähettäjä
            }

            existing.Title = message.Title;
            existing.Content = message.Content;

            await _repository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteMessageAsync(int id, int userId)
        {
            var existing = await _repository.GetByIdAsync(id); // haetaan viesti tietokannasta

            if (existing == null || existing.SenderId != userId) // vain oman viestin saa poistaa
            {
                return false;
            }

            await _repository.DeleteAsync(existing);
            return true;
        }
    }
}

