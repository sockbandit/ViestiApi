using ViestiApi.Models;

namespace ViestiApi.Services
{
    public interface IMessageService
    {
        /*List<Message> GetPublicMessages(); // Hakee kaikki julkiset viestit   
        void SendMessage(Message message); // Lähettää uuden viestin*/              //Vanha koodi, joka on korvattu asynkronisilla metodeilla ja lisätty käyttäjän oikeuksien tarkistus. UserId-parametri on lisätty metodeihin, jotka vaativat käyttäjän oikeuksien tarkistuksen.
        Task<List<MessageDTO>> GetPublicMessagesAsync(); // Hakee kaikki julkiset viestit asynkronisesti
        Task<List<MessageDTO>> GetMessagesForUserAsync(int userId); // Hakee kaikki viestit tietylle käyttäjälle asynkronisesti
        Task<MessageDTO?> GetMessageAsync(long id, int userId); // Hakee yksittäisen viestin id:n perusteella asynkronisesti, mutta tarkistaa myös, että käyttäjä on oikeutettu näkemään viestin
        Task<MessageDTO?> SendMessageAsync(MessageDTO message); // Lähettää uuden viestin asynkronisesti
        Task<bool> UpdateMessageAsync(MessageDTO message, int userId); // Päivittää olemassa olevan viestin tiedot asynkronisesti, mutta tarkistaa myös, että käyttäjä on oikeutettu muokkaamaan viestiä
        Task<bool> DeleteMessageAsync(long id, int userId); // Poistaa viestin id:n perusteella asynkronisesti, mutta tarkistaa myös, että käyttäjä on oikeutettu poistamaan viestin
    }
}
