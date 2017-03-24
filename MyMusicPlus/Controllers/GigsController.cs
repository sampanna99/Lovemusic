using Microsoft.AspNet.Identity;
using MyMusicPlus.Models;
using MyMusicPlus.ViewModel;
using System;
using System.Data.Entity;
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
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances.Where(a => a.AttendeeId == userId).Select(a => a.Gig).Include(g => g.Artist).
                Include(g => g.Genre).ToList();
            var viewModel = new HomeViewModel { UpcomingGigs = gigs, ShowActions = User.Identity.IsAuthenticated, Heading = "Gigs I'm Attending" };
            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs.Where(g => g.ArtistId == userId && g.Datetime > DateTime.Now).
                Include(g => g.Genre).ToList();

            return View(gigs);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel();
            viewModel.Genres = _context.Genres.ToList();
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();

                return View("Create", viewModel);
            }
            var artistId = User.Identity.GetUserId();
            //var artist = _context.Users.Single(u => u.Id == artistId);
            //var genre = _context.Genres.Single(g => g.Id == viewModel.Genre);
            var gig = new Gig
            {
                ArtistId = artistId,
                Datetime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");
        }
    }
}