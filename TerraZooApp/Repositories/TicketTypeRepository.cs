using Microsoft.EntityFrameworkCore;
using TerraZooApp.Data;
using TerraZooApp.Models;

namespace TerraZooApp.Repositories
{
    public class TicketTypeRepository : ITicketTypeRepository
    {
        private readonly TerraZooAppDbContext _context;

        public TicketTypeRepository(TerraZooAppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TicketType> GetAll()
        {
            return _context.TicketTypes.ToList();
        }

        public void Create(TicketType ticketType)
        {
            _context.TicketTypes.Add(ticketType);
            _context.SaveChanges();
        }

        public void Update(TicketType ticketType)
        {
            var existing = _context.TicketTypes.Find(ticketType.IdTicketType);
            if (existing == null)
                throw new KeyNotFoundException($"Ticket type with ID {ticketType.IdTicketType} not found.");

            // Actualizează doar proprietățile editabile
            existing.NameTicketType = ticketType.NameTicketType;
            existing.Price = ticketType.Price;

            _context.SaveChanges();
        }

        public TicketType GetById(int id)
        {
            return _context.TicketTypes.Find(id);
        }
    }
} 