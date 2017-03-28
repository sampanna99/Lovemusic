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
            viewModel.Heading = "Add a Gig";
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();

                return View("GigForm", viewModel);
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

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(a => a.Id == id && a.ArtistId == userId);

            var viewModel = new GigFormViewModel
            {
                Heading = "Edit a Gig",
                Id = gig.Id,
                Genres = _context.Genres.ToList(),
                Date = gig.Datetime.ToString("d MMM yyyy"),
                Time = gig.Datetime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();

                return View("GigForm", viewModel);
            }
            var userId = User.Identity.GetUserId();
            //var artist = _context.Users.Single(u => u.Id == artistId);
            //var genre = _context.Genres.Single(g => g.Id == viewModel.Genre);
            var gig = _context.Gigs.Single(g => g.Id == viewModel.Id && g.ArtistId == userId);
            gig.Venue = viewModel.Venue;
            gig.Datetime = viewModel.GetDateTime();
            gig.GenreId = viewModel.Genre;
            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");
        }

    }
}