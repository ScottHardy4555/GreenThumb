using GreenThumb.Data.Services;
using GreenThumb.Models.DataLayer;
using DailyLogM = GreenThumb.Models.DomainModels.DailyLog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GreenThumb.Models;

namespace GreenThumb.Areas.DailyLog.Controllers
{
    [Area("DailyLog")]
    public class DailyLogController : Controller
	{
		private Repository<DailyLogM> DailyLogs { get; set; }
		public DailyLogController(ProjectContext ctx) => DailyLogs = new Repository<DailyLogM>(ctx);

		[NonAction]
        public string GetSlug(string s) => s.Replace(" ", "-").ToLower();

		[Authorize(Roles = "Admin")]
		[HttpGet]
        public IActionResult Delete(int id)
        {
            var log = DailyLogs.Get(id);
            return View(log);
        }

		[Authorize(Roles = "Admin")]
		[HttpPost]
        public IActionResult Delete(DailyLogM log)
        {
			DailyLogs.Delete(log);
			DailyLogs.Save();
			TempData["successMessage"] = $"{log.GetDateFormatted()} was deleted.";
			return RedirectToAction("List", "DailyLog");
        }

		[Authorize(Roles = "Admin")]
		[HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add new daily log";
            return View("Edit", new DailyLogM());
        }

		[Authorize(Roles = "Admin")]
		[HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit log";
            var log = DailyLogs.Get(id);
            return View(log);
        }

		[Authorize(Roles = "Admin")]
		[HttpPost]
        public IActionResult Edit(DailyLogM log)
        {
            if (ModelState.IsValid)
            {
                if (log.DailyLogId == 0)
                {
					DailyLogs.Insert(log);
                    DailyLogs.Save();
                    TempData["successMessage"] = $"{log.GetDateFormatted()} was added.";
                    return RedirectToAction("List");
                }
                else
                {
					DailyLogs.Update(log);
					TempData["successMessage"] = $"{log.GetDateFormatted()} was modified.";
					return RedirectToAction("View", "StatusReport", new { id = log.DailyLogId });
                }
            }
            else
            {
                ViewBag.Action = log.DailyLogId == 0 ? "Add" : "Edit";
                return View(log);
            }
        }

        [HttpGet]
        public IActionResult List(string id = "All")
        {
            ViewBag.Action = "View logs";

			var dailyLogOptions = new QueryOptions<DailyLogM>()
			{
				OrderBy = c => c.DailyLogId
			};

			List<DailyLogM> dailyLogs = DailyLogs.List(dailyLogOptions).ToList();
            return View(dailyLogs);
        }

        //Possibly add Index() method?
    }
}
