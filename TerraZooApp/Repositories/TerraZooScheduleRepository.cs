using System;
using TerraZooApp.Data;
using TerraZooApp.Models;

namespace TerraZooApp.Repositories
{
    public class TerraZooScheduleRepository : ITerraZooScheduleRepository
    {
        private readonly TerraZooAppDbContext _context;

        public TerraZooScheduleRepository(TerraZooAppDbContext context)
        {
            _context = context;
        }

        public List<TerraZooSchedule> GetAll()
        {
            return _context.TerraZooSchedules.ToList();
        }

        public TerraZooSchedule? GetById(int id)
        {
            return _context.TerraZooSchedules.Find(id);
        }

        public void Create(TerraZooSchedule schedule)
        {
            _context.TerraZooSchedules.Add(schedule);
            _context.SaveChanges();
        }

        public void Update(TerraZooSchedule schedule)
        {
            _context.TerraZooSchedules.Update(schedule);
            _context.SaveChanges();
        }

    }
}
