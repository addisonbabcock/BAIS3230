using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Identity;
using Microsoft.Data.Entity;
using GolfCourseManager.Models;
using GolfCourseManager.ViewModels;
using System.Security.Claims;
using System.Collections.Generic;

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
        public async Task<IActionResult> Create()
        {
			var vm = new EnterScoreViewModel();
			var member = await _gcmRepo.GetLoggedInMemberAsync(User);
			var teeTimes = _gcmRepo.GetTeeTimesWithoutScore(member);

			vm.SelectableTeeTimes = new List<System.DateTime>();
			foreach (var teeTime in teeTimes)
			{
				vm.SelectableTeeTimes.Add(teeTime.Start);
			}

            return View(vm);
        }

        // POST: Scores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnterScoreViewModel vm)
        {
            if (ModelState.IsValid)
            {
				var member = await _gcmRepo.GetLoggedInMemberAsync(User);
				var score = new Score();
				score.Member = member;
				score.GolfCourse = _gcmRepo.GetGolfCourse();
				score.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 1);
				score.PlayerName = member.GetFullName();
				score.Strokes = vm.Hole1;
				_gcmRepo.AddScore(score);
				await _gcmRepo.SaveScoresAsync();
                return RedirectToAction("Index");
            }
            return View(vm);
        }
    }
}
