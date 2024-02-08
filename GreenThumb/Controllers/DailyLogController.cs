using Microsoft.AspNetCore.Mvc;
using GreenThumb.Models;

namespace GreenThumb.Controllers
{
	public class DailyLogController : Controller
	{
		private ProjectContext _context { get; set; }
		public DailyLogController(ProjectContext ctx)
		{
			_context = ctx;
		}

		//View Delete Log Page
		[HttpGet]
		public IActionResult Delete(int id)
		{
			var log = _context.DailyLogs.Find(id);
			return View(log);
		}

		//Delete Log by Id
		[HttpPost]
		public IActionResult Delete(DailyLog log)
		{
			_context.DailyLogs.Remove(log);
			_context.SaveChanges();
			return RedirectToAction("List", "DailyLog");
		}

		[HttpGet]
		public IActionResult Add()
		{
			ViewBag.Action = "Add new daily log";
			return View("Edit", new DailyLog());
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			ViewBag.Action = "Edit log";
			var log = _context.DailyLogs.Find(id);
			return View(log);
		}

		[HttpPost]
		public IActionResult Edit(DailyLog log)
		{
			if (ModelState.IsValid)
			{
				if (log.DailyLogId == 0)
				{
					_context.DailyLogs.Add(log);
					_context.SaveChanges();
					return RedirectToAction("List");
				}
				else
				{
					_context.DailyLogs.Update(log);
					_context.SaveChanges();
					return RedirectToAction("View", "StatusReport", new { id = log.DailyLogId });
				}
			}
			else
			{
				ViewBag.Action = (log.DailyLogId == 0) ? "Add" : "Edit";
				return View(log);
			}
		}

		[HttpGet]
		public IActionResult List()
		{
			ViewBag.Action = "View logs";
			var dailyLogs = _context.DailyLogs.ToList();
			return View(dailyLogs);
		}

		//Possibly add Index() method?
	}
}
