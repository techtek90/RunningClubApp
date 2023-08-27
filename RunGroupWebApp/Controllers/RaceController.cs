using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Controllers
{
    public class RaceController : Controller
    {
        //private readonly AppDbContext _context;
        private readonly IRaceRepo _raceRepo;
        public RaceController(IRaceRepo raceRepo)
        {
            _raceRepo = raceRepo;
        }
        public async Task <IActionResult> Index()
        {
            //var races = _context.Races.ToList();
            var races = await _raceRepo.GetAllRaces();
            return View(races);
        }
        public async Task <IActionResult> Detail(int id)
        {
            //Race race = _context.Races.Include(a => a.Address).FirstOrDefault(r => r.Id == id);
            Race race = await _raceRepo.GetRaceById(id);
            return View(race);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Race race)
        {
            if (!ModelState.IsValid)
                return View();
            _raceRepo.Add(race);
            return RedirectToAction("Index");

        }

    }
}
