using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using GolfCourseManager.Models;

namespace GolfCourseManager.Controllers
{
    public class ScoresController : Controller
    {
        private GCMContext _context;

        public ScoresController(GCMContext context)
        {
            _context = context;    
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
