using TerraZooApp.Models;
using TerraZooApp.Repositories;

namespace TerraZooApp.Services
{
    public class TicketTypeService : ITicketTypeService
    {
        private readonly ITicketTypeRepository _repository;

        public TicketTypeService(ITicketTypeRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TicketType> GetAll()
        {
            return _repository.GetAll();
        }

        public void Create(TicketType ticketType)
        {
            if (ticketType == null)
                throw new ArgumentNullException(nameof(ticketType));

            _repository.Create(ticketType);
        }

        public void Update(TicketType ticketType)
        {
            if (ticketType == null)
                throw new ArgumentNullException(nameof(ticketType));

            var existingTicketType = _repository.GetById(ticketType.IdTicketType);
            if (existingTicketType == null)
                throw new KeyNotFoundException($"Tipul de bilet cu ID-ul {ticketType.IdTicketType} nu a fost găsit.");

            _repository.Update(ticketType);
        }

        public TicketType GetById(int id)
        {
            return _repository.GetById(id);
        }
    }
} 