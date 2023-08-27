using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Repository
{
    public class RaceRepo : IRaceRepo
    {
        private readonly AppDbContext _context;
        public RaceRepo(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(Race race)
        {
            _context.Add(race);
            return Save();
        }

        public async Task<IEnumerable<Race>> GetAllRaces()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<IEnumerable<Race>> GetAllracesByCity(string city)
        {
            return await _context.Races.Where(c => c.Address.City == city).ToListAsync();   
        }

        public async Task<Race> GetRaceById(int id)
        {
            return await _context.Races.Include(r => r.Address).FirstOrDefaultAsync();   
        }
        
        public bool Remove(Race race)
        {
            _context.Remove(race);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return false;
        }

        public bool Update(Race race)
        {
            _context.Update(race);
            return Save();
        }
    }
}
