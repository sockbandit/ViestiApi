using ViestiApi.Models;

namespace ViestiApi.Repositories
{
    public interface IMessageRepository
    {
        //---------VANHAT ENNEN ASYNC/TESTI---------
        /*List<Message> GetAll(); // Hakee kaikki viestit tietokannasta
        Message? GetById(int id); // Hakee viestin tietokannasta ID:n perusteella
        void Add(Message message); // Lisää uusi viesti tietokantaan*/
        //-----------------------------------------------------

        Task<List<Message>> GetAllAsync(); // Hakee kaikki viestit tietokannasta asynkronisesti
        Task<Message?> GetByIdAsync(long id); // Hakee viestin tietokannasta ID:n perusteella asynkronisesti
        Task AddAsync(Message message); // Lisää uusi viesti tietokantaan asynkronisesti
        Task UpdateAsync(Message message); // Päivittää olemassa olevan viestin tiedot tietokannassa asynkronisesti
        Task DeleteAsync(Message message); // Poistaa viestin tietokannasta asynkronisesti
    }
}
