using System.Linq;
using System.Threading.Tasks;
using GolfCourseManager.Models;
using GolfCourseManager.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace GolfCourseManager.Controllers
{
    public class ScoresController : Controller
    {
		private GCMRepository _gcmRepo;

		public ScoresController(GCMRepository repo)
		{
			_gcmRepo = repo;
        }

        // GET: Scores
        public async Task<IActionResult> Index()
        {
			var vm = new ScoresIndexViewModel();
			vm.Results = new List<ScoresIndexViewModel.Row>();
			vm.GolfCourseId = _gcmRepo.GetGolfCourse().Id;

			List<TeeTime> teeTimes;
			if (User.IsInRole("admin"))
			{
				teeTimes = _gcmRepo.GetTeeTimesWithScore();
				teeTimes.Sort(delegate (TeeTime a, TeeTime b)
				{
					return a.Start.CompareTo(b.Start);
				});
			}
			else
			{
				var member = await _gcmRepo.GetLoggedInMemberAsync(User);
				teeTimes = _gcmRepo.GetTeeTimesWithScore(member);
			}

			foreach (var teeTime in teeTimes)
			{
				var result = new ScoresIndexViewModel.Row();
				result.MemberName = teeTime.Member.GetFullName();
				result.StartTime = teeTime.Start;
				result.TeeTimeId = teeTime.Id;
				var scores = _gcmRepo.GetScoresForTeeTime(teeTime);

				foreach (var score in scores)
				{
					result.Strokes += score.Strokes;
				}

				vm.Results.Add(result);
			}

			return View(vm);
        }

		[HttpGet]
		[Route("Scores/Details/{id}")]
        public IActionResult Details(int id)
        {
			var vm = new EnterScoreViewModel();
			var teeTime = _gcmRepo.GetTeeTime(id);
			var scores = _gcmRepo.GetScoresForTeeTime(teeTime);

			vm.TeeTime = teeTime.Start;
			vm.Hole1 = scores.Where(score => score.Hole.HoleNumber == 1).FirstOrDefault().Strokes;
			vm.Hole2 = scores.Where(score => score.Hole.HoleNumber == 2).FirstOrDefault().Strokes;
			vm.Hole3 = scores.Where(score => score.Hole.HoleNumber == 3).FirstOrDefault().Strokes;
			vm.Hole4 = scores.Where(score => score.Hole.HoleNumber == 4).FirstOrDefault().Strokes;
			vm.Hole5 = scores.Where(score => score.Hole.HoleNumber == 5).FirstOrDefault().Strokes;
			vm.Hole6 = scores.Where(score => score.Hole.HoleNumber == 6).FirstOrDefault().Strokes;
			vm.Hole7 = scores.Where(score => score.Hole.HoleNumber == 7).FirstOrDefault().Strokes;
			vm.Hole8 = scores.Where(score => score.Hole.HoleNumber == 8).FirstOrDefault().Strokes;
			vm.Hole9 = scores.Where(score => score.Hole.HoleNumber == 9).FirstOrDefault().Strokes;
			vm.Hole10 = scores.Where(score => score.Hole.HoleNumber == 10).FirstOrDefault().Strokes;
			vm.Hole11 = scores.Where(score => score.Hole.HoleNumber == 11).FirstOrDefault().Strokes;
			vm.Hole12 = scores.Where(score => score.Hole.HoleNumber == 12).FirstOrDefault().Strokes;
			vm.Hole13 = scores.Where(score => score.Hole.HoleNumber == 13).FirstOrDefault().Strokes;
			vm.Hole14 = scores.Where(score => score.Hole.HoleNumber == 14).FirstOrDefault().Strokes;
			vm.Hole15 = scores.Where(score => score.Hole.HoleNumber == 15).FirstOrDefault().Strokes;
			vm.Hole16 = scores.Where(score => score.Hole.HoleNumber == 16).FirstOrDefault().Strokes;
			vm.Hole17 = scores.Where(score => score.Hole.HoleNumber == 17).FirstOrDefault().Strokes;
			vm.Hole18 = scores.Where(score => score.Hole.HoleNumber == 18).FirstOrDefault().Strokes;

			return View(vm);
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

				var score1 = new Score();
				score1.Member = member;
				score1.GolfCourse = _gcmRepo.GetGolfCourse();
				score1.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 1);
				score1.PlayerName = member.GetFullName();
				score1.TeeTime = _gcmRepo.GetTeeTime(vm.TeeTime);
				score1.Strokes = vm.Hole1;
				_gcmRepo.AddScore(score1);

				var score2 = new Score();
				score2.Member = member;
				score2.GolfCourse = _gcmRepo.GetGolfCourse();
				score2.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 2);
				score2.PlayerName = member.GetFullName();
				score2.TeeTime = _gcmRepo.GetTeeTime(vm.TeeTime);
				score2.Strokes = vm.Hole2;
				_gcmRepo.AddScore(score2);

				var score3 = new Score();
				score3.Member = member;
				score3.GolfCourse = _gcmRepo.GetGolfCourse();
				score3.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 3);
				score3.PlayerName = member.GetFullName();
				score3.TeeTime = _gcmRepo.GetTeeTime(vm.TeeTime);
				score3.Strokes = vm.Hole3;
				_gcmRepo.AddScore(score3);

				var score4 = new Score();
				score4.Member = member;
				score4.GolfCourse = _gcmRepo.GetGolfCourse();
				score4.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 4);
				score4.PlayerName = member.GetFullName();
				score4.TeeTime = _gcmRepo.GetTeeTime(vm.TeeTime);
				score4.Strokes = vm.Hole4;
				_gcmRepo.AddScore(score4);

				var score5 = new Score();
				score5.Member = member;
				score5.GolfCourse = _gcmRepo.GetGolfCourse();
				score5.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 5);
				score5.PlayerName = member.GetFullName();
				score5.TeeTime = _gcmRepo.GetTeeTime(vm.TeeTime);
				score5.Strokes = vm.Hole5;
				_gcmRepo.AddScore(score5);

				var score6 = new Score();
				score6.Member = member;
				score6.GolfCourse = _gcmRepo.GetGolfCourse();
				score6.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 6);
				score6.PlayerName = member.GetFullName();
				score6.TeeTime = _gcmRepo.GetTeeTime(vm.TeeTime);
				score6.Strokes = vm.Hole6;
				_gcmRepo.AddScore(score6);

				var score7 = new Score();
				score7.Member = member;
				score7.GolfCourse = _gcmRepo.GetGolfCourse();
				score7.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 7);
				score7.PlayerName = member.GetFullName();
				score7.TeeTime = _gcmRepo.GetTeeTime(vm.TeeTime);
				score7.Strokes = vm.Hole7;
				_gcmRepo.AddScore(score7);

				var score8 = new Score();
				score8.Member = member;
				score8.GolfCourse = _gcmRepo.GetGolfCourse();
				score8.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 8);
				score8.PlayerName = member.GetFullName();
				score8.TeeTime = _gcmRepo.GetTeeTime(vm.TeeTime);
				score8.Strokes = vm.Hole8;
				_gcmRepo.AddScore(score8);

				var score9 = new Score();
				score9.Member = member;
				score9.GolfCourse = _gcmRepo.GetGolfCourse();
				score9.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 9);
				score9.PlayerName = member.GetFullName();
				score9.TeeTime = _gcmRepo.GetTeeTime(vm.TeeTime);
				score9.Strokes = vm.Hole9;
				_gcmRepo.AddScore(score9);

				var score10 = new Score();
				score10.Member = member;
				score10.GolfCourse = _gcmRepo.GetGolfCourse();
				score10.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 10);
				score10.PlayerName = member.GetFullName();
				score10.TeeTime = _gcmRepo.GetTeeTime(vm.TeeTime);
				score10.Strokes = vm.Hole10;
				_gcmRepo.AddScore(score10);

				var score11 = new Score();
				score11.Member = member;
				score11.GolfCourse = _gcmRepo.GetGolfCourse();
				score11.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 11);
				score11.PlayerName = member.GetFullName();
				score11.TeeTime = _gcmRepo.GetTeeTime(vm.TeeTime);
				score11.Strokes = vm.Hole11;
				_gcmRepo.AddScore(score11);

				var score12 = new Score();
				score12.Member = member;
				score12.GolfCourse = _gcmRepo.GetGolfCourse();
				score12.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 12);
				score12.PlayerName = member.GetFullName();
				score12.TeeTime = _gcmRepo.GetTeeTime(vm.TeeTime);
				score12.Strokes = vm.Hole12;
				_gcmRepo.AddScore(score12);

				var score13 = new Score();
				score13.Member = member;
				score13.GolfCourse = _gcmRepo.GetGolfCourse();
				score13.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 13);
				score13.PlayerName = member.GetFullName();
				score13.TeeTime = _gcmRepo.GetTeeTime(vm.TeeTime);
				score13.Strokes = vm.Hole13;
				_gcmRepo.AddScore(score13);

				var score14 = new Score();
				score14.Member = member;
				score14.GolfCourse = _gcmRepo.GetGolfCourse();
				score14.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 14);
				score14.PlayerName = member.GetFullName();
				score14.TeeTime = _gcmRepo.GetTeeTime(vm.TeeTime);
				score14.Strokes = vm.Hole14;
				_gcmRepo.AddScore(score14);

				var score15 = new Score();
				score15.Member = member;
				score15.GolfCourse = _gcmRepo.GetGolfCourse();
				score15.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 15);
				score15.PlayerName = member.GetFullName();
				score15.TeeTime = _gcmRepo.GetTeeTime(vm.TeeTime);
				score15.Strokes = vm.Hole15;
				_gcmRepo.AddScore(score15);

				var score16 = new Score();
				score16.Member = member;
				score16.GolfCourse = _gcmRepo.GetGolfCourse();
				score16.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 16);
				score16.PlayerName = member.GetFullName();
				score16.TeeTime = _gcmRepo.GetTeeTime(vm.TeeTime);
				score16.Strokes = vm.Hole16;
				_gcmRepo.AddScore(score16);

				var score17 = new Score();
				score17.Member = member;
				score17.GolfCourse = _gcmRepo.GetGolfCourse();
				score17.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 17);
				score17.PlayerName = member.GetFullName();
				score17.TeeTime = _gcmRepo.GetTeeTime(vm.TeeTime);
				score17.Strokes = vm.Hole17;
				_gcmRepo.AddScore(score17);

				var score18 = new Score();
				score18.Member = member;
				score18.GolfCourse = _gcmRepo.GetGolfCourse();
				score18.Hole = _gcmRepo.GetHole(_gcmRepo.GetGolfCourse(), 18);
				score18.PlayerName = member.GetFullName();
				score18.TeeTime = _gcmRepo.GetTeeTime(vm.TeeTime);
				score18.Strokes = vm.Hole18;
				_gcmRepo.AddScore(score18);

				await _gcmRepo.SaveScoresAsync();
                return RedirectToAction("Index");
            }
            return View(vm);
        }
    }
}
