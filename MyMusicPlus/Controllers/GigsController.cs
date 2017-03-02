using MyMusicPlus.Models;
using MyMusicPlus.ViewModel;
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
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel();
            viewModel.Genres = _context.Genres.ToList();
            return View(viewModel);
        }
    }
}