﻿using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Repository
{
    public class ClubRepo : IClubRepo
    {
        private readonly AppDbContext _context;
        public ClubRepo(AppDbContext context)
        {
            _context = context;           
        }
        public bool Add(Club club)
        {     
            _context.Add(club);
            return Save();
        }
        public bool Delete(Club club)
        {
            _context.Remove(club);
            return Save();
        }
        public async Task<IEnumerable<Club>> GetAllClub()
        {
           return await _context.Clubs.ToListAsync();
            
        }

        public async Task<Club> GetClubById(int id)
        {
            return await _context.Clubs.Include(c => c.Address).FirstOrDefaultAsync(i=> i.Id == id);
        }       

        public async Task<IEnumerable<Club>> GetClubsByCity(string cityName)
        {
            return await _context.Clubs.Where(c => c.Address.City.Contains(cityName)).ToListAsync();
        }

        public bool Save()
        {
            var saved= _context.SaveChanges();
            return saved>0 ? true : false;
        }

        public bool Update(Club club)
        {
            _context.Clubs.Update(club);
            return Save();
        }
    }
}