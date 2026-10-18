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

        public async Task<List<MessageDTO>> GetPublicMessagesAsync() // palauttaa kaikki julkiset viestit (joilla ei ole vastaanottajaa)
        {
            var messages = await _repository.GetAllAsync();
            return messages
                .Where(m => m.ReceiverId == null) // suodatetaan viestit, joilla ei ole vastaanottajaa
                .Select(m => MessageToDTO(m))
                .ToList(); // palautetaan viestit listana

        }
        public async Task<List<MessageDTO>> GetMessagesForUserAsync(int userId) // palauttaa kaikki viestit jotka käyttäjä voi nähdä
        {
            var messages = await _repository.GetAllAsync();
            return messages
                .Where(m => m.ReceiverId == null // suodatetaan viestit, joilla ei ole vastaanottajaa
                    || m.SenderId == userId // tai viestit, jotka käyttäjä on lähettänyt
                    || m.ReceiverId == userId) // tai viestit, jotka käyttäjä on vastaanottanut
                .Select(m => MessageToDTO(m))
                .ToList(); // palautetaan viestit listana
        }
        public async Task<MessageDTO?> GetMessageAsync(long id, int userId) // palauttaa yksittäisen viestin id:n perusteella, mutta tarkistaa myös, että käyttäjä on oikeutettu näkemään viestin
        {
            var message = await _repository.GetByIdAsync(id);
            if (message == null)
            {
                return null;
            }

            bool canSee = message.ReceiverId == null //cansee tarkoittaa että käyttäjä voi nähdä viestin jos se on julkinen (ei vastaanottajaa)
                       || message.SenderId == userId //tai jos käyttäjä on viestin lähettäjä
                       || message.ReceiverId == userId; //tai jos käyttäjä on viestin vastaanottaja :)

            return canSee ? MessageToDTO(message) : null; // jos käyttäjä ei voi nähdä viestiä, palautetaan null
        }

        public async Task<MessageDTO?> SendMessageAsync(MessageDTO message) // viestin lähettäminen 
        {
            if (message.PreviousMessageId != null) // tarkistetaan, onko viesti vastaus johonkin vertaamalla viestin PreviousMessageId:hen, jos se on null, niin viesti ei ole vastaus
            {
                var previous = await _repository.GetByIdAsync(message.PreviousMessageId.Value);
                if (previous == null)
                {
                    return null;
                }
            }

            var newMessage = DTOToMessage(message); // luodaan uusi viesti DTO:sta
            newMessage.SendTime = DateTime.UtcNow; // asetetaan viestin lähetysaika UTC-aikaan

            await _repository.AddAsync(newMessage); // lisätään viesti tietokantaan
            return MessageToDTO(newMessage); // palautetaan viesti 
        }

        public async Task<bool> UpdateMessageAsync(MessageDTO message, int userId)
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

        public async Task<bool> DeleteMessageAsync(long id, int userId)
        {
            var existing = await _repository.GetByIdAsync(id); // haetaan viesti tietokannasta

            if (existing == null || existing.SenderId != userId) // vain oman viestin saa poistaa
            {
                return false;
            }

            await _repository.DeleteAsync(existing);
            return true;
        }

        private MessageDTO MessageToDTO(Message message) // Muuntaa Message-olion MessageDTO-olioksi, jotta voidaan palauttaa viesti asiakkaalle
        {
            return new MessageDTO
            {
                Id = message.Id,
                Title = message.Title,
                Content = message.Content,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                PreviousMessageId = message.PreviousMessageId,
                SendTime = message.SendTime
            };
        }

        private Message DTOToMessage(MessageDTO dto) // Muuntaa MessageDTO-olion Message-olioksi, jotta voidaan tallentaa viesti tietokantaan
        {
            return new Message
            {
                Title = dto.Title,
                Content = dto.Content,
                SenderId = dto.SenderId,
                ReceiverId = dto.ReceiverId,
                PreviousMessageId = dto.PreviousMessageId
            };
        }
    }
}

