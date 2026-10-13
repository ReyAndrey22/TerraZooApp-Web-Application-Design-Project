using TerraZooApp.Models;

namespace TerraZooApp.Services
{
    public interface ITicketTypeService
    {
        IEnumerable<TicketType> GetAll();
        void Create(TicketType ticketType);
        void Update(TicketType ticketType);
        TicketType GetById(int id);
    }
} 