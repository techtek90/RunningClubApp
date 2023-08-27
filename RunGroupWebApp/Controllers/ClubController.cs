using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.ViewModels;

namespace RunGroupWebApp.Controllers
{
     public class ClubController:Controller
    {
        //private readonly AppDbContext _context;
        private readonly IClubRepo _clubRepo;
        private readonly IPhotoService _photoService;
        public ClubController(IClubRepo clubRepo, IPhotoService photoService)
        {            
            _clubRepo = clubRepo;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()   //Controller
        {
            //var clubs = _context.Clubs.ToList(); //Model
            var clubs = _clubRepo.GetAllClub();
            return View(clubs);                    //View
        }

        public async Task<IActionResult> Detail(int id)
        {
            //Club club = _context.Clubs.Include(a => a.Address).FirstOrDefault(c => c.Id == id);
            Club club = await _clubRepo.GetClubById(id);
            return View(club);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if(ModelState.IsValid)
            {
                var sonuc = await _photoService.AddPhotoAsync(clubVM.Image);
                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = sonuc.Url.ToString(),
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State
                    }
                };
                _clubRepo.Add(club);
                return RedirectToAction("Index");
            }                        
            else
            {
                ModelState.AddModelError("", "Photo upload failed");               
            }
            return View(clubVM);
        }
    }
}
