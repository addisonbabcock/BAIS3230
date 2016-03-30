using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using GolfCourseManager.Models;
using GolfCourseManager.ViewModels;

namespace GolfCourseManager.Controllers
{
    public class ScoresController : Controller
    {
        private GCMContext _context;			//TODO: Refactor to not use the context
		private GCMRepository _gcmRepo;

		public ScoresController(GCMContext context, GCMRepository repo)
        {
            _context = context;
			_gcmRepo = repo;
        }

        // GET: Scores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Scores.ToListAsync());
        }

        // GET: Scores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Score score = await _context.Scores.SingleAsync(m => m.Id == id);
            if (score == null)
            {
                return HttpNotFound();
            }

            return View(score);
        }

        // GET: Scores/Create
        public IActionResult Create()
        {
			var vm = new EnterScoreViewModel();
			Member member;			//TODO: Grab the current user
			var teeTimes = _gcmRepo.GetTeeTimesWithoutScore(member);
            return View();
        }

        // POST: Scores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Score score)
        {
            if (ModelState.IsValid)
            {
                _context.Scores.Add(score);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(score);
        }
    }
}
