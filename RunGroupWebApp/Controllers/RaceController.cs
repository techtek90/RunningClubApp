using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.ViewModels;

namespace RunGroupWebApp.Controllers
{
    public class RaceController : Controller
    {
        //private readonly AppDbContext _context;
        private readonly IRaceRepo _raceRepo;
        private readonly IPhotoService _photoService;
        public RaceController(IRaceRepo raceRepo, IPhotoService photoService)
        {
            _raceRepo = raceRepo;
            _photoService = photoService;
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
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var resimYuklemeSonucu = await _photoService.AddPhotoAsync(raceVM.Image);
                var race = new Race()
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = resimYuklemeSonucu.Url.ToString(),
                    Address = new Address()
                    {
                        City = raceVM.Address.City,
                        State = raceVM.Address.State,
                        Street = raceVM.Address.Street
                    }
                };
                _raceRepo.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(raceVM);

        }

    }
}
