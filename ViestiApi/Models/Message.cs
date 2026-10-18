using System.ComponentModel.DataAnnotations;

namespace ViestiApi.Models
{
    public class Message // Message-luokka, joka kuvaa viestiä tietokannassa, MessageDTO-luokka, joka kuvaa viestiä tietokannassa ja siirtää dataa palvelimen ja asiakkaan välillä
    {
        public long Id { get; set; } // Tallentaa viestin ID:n tietokantaan
        [MaxLength(100)] 
        public string Title { get; set; } = ""; 
        public string Content { get; set; } = ""; 
        public int SenderId { get; set; } 
        public int? ReceiverId { get; set; } 
        public long? PreviousMessageId { get; set; } 
        public DateTime? SendTime { get; set; } // Tallentaa viestin lähetysaikaan tietokantaan, ?=nullable, koska viesti voi olla luonnos

        //senderid, receiverid ja previousmessageid on vierasavaimia eli ne viittaa toisen olion id:seen.

    }

    public class MessageDTO // DTO = Data Transfer Object, eli olio, joka siirtää dataa palvelimen ja asiakkaan välillä, tässä tapauksessa se on tarkoitettu viestin näyttämiseen, joten se sisältää myös lähettäjän ja vastaanottajan käyttäjätiedot
    {
        public long Id { get; set; }
        [MaxLength(100)]

        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public int SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public long? PreviousMessageId { get; set; }
        public DateTime? SendTime { get; set; }

    }
}
