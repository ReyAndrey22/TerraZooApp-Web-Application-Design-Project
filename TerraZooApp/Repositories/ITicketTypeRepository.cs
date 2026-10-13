using TerraZooApp.Models;

namespace TerraZooApp.Repositories
{
    public interface ITicketTypeRepository
    {
        IEnumerable<TicketType> GetAll();
        void Create(TicketType ticketType);
        void Update(TicketType ticketType);
        TicketType GetById(int id);
    }
} 