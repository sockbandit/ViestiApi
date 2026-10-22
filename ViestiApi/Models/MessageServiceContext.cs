using Microsoft.EntityFrameworkCore;
namespace ViestiApi.Models
{
    public class MessageServiceContext : DbContext
    {
        public MessageServiceContext(DbContextOptions<MessageServiceContext> options) : base(options) // Konstruktori, joka ottaa vastaan DbContextOptions ja välittää ne DbContext-luokalle
        {
        }

        public DbSet<User> Users => Set<User>(); // Määrittelee Users DbSetin, joka vastaa User-taulua tietokannassa
        public DbSet<Message> Messages => Set<Message>(); // Määrittelee Messages DbSetin, joka vastaa Message-taulua tietokannassa 
    }
}
