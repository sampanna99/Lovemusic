using Microsoft.AspNet.Identity;
using MyMusicPlus.Models;
using MyMusicPlus.ViewModel;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyMusicPlus.Controllers
{
    public class GigsController : Controller
    {
        private ApplicationDbContext _context;


        public GigsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Gigs
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel();
            viewModel.Genres = _context.Genres.ToList();
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            var artistId = User.Identity.GetUserId();
            //var artist = _context.Users.Single(u => u.Id == artistId);
            //var genre = _context.Genres.Single(g => g.Id == viewModel.Genre);
            var gig = new Gig
            {
                ArtistId = artistId,
                Datetime = DateTime.Parse(string.Format("{0}{1}", viewModel.Date, viewModel.Time)),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
    }
}