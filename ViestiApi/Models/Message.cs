namespace ViestiApi.Models
{
    public class Message
    {
        public int Id { get; set; } // Tallentaa viestin ID:n tietokantaan
        public string Title { get; set; } = ""; // Tallentaa viestin otsikon tietokantaan
        public string Content { get; set; } = ""; // Tallentaa viestin sisällön tietokantaan
        public int SenderId { get; set; } // Tallentaa lähettäjän ID:n tietokantaan
        public int? ReceiverId { get; set; } // Tallentaa vastaanottajan ID:n tietokantaan, ?=nullable, koska viesti voi olla julkinen
        public int? PreviousMessageId { get; set; } // Tallentaa edellisen viestin ID:n tietokantaan, ?=nullable, koska viesti voi olla ensimmäinen viesti ketjussa

        //Kolme alinta eli senderid, receiverid ja previousmessageid on vierasavaimia eli ne viittaa toisen olion id:seen.
    }
}
