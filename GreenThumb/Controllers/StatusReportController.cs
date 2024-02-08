using GreenThumb.Models;
using Microsoft.AspNetCore.Mvc;

namespace GreenThumb.Controllers
{
	public class StatusReportController : Controller
	{
		private ProjectContext _context { get; set; }
		public StatusReportController(ProjectContext ctx)
		{
			_context = ctx;
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var log = _context.StatusReports.Find(id);
			return View(log);
		}

		[HttpPost]
		public IActionResult Delete(StatusReport report)
		{
			_context.StatusReports.Remove(report);
			_context.SaveChanges();
			return RedirectToAction("List", "DailyLog");
		}

		[HttpGet]
		public IActionResult Add(int dailyLogId)
		{
			ViewBag.Action = "Add new hourly report";
			StatusReport report = new StatusReport() { DailyLogId = dailyLogId };
			return View("Edit", report);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			ViewBag.Action = "Edit log";
			var log = _context.StatusReports.Find(id);
			return View(log);
		}

		[HttpPost]
		public IActionResult Edit(StatusReport report)
		{
			if (ModelState.IsValid)
			{
				if (report.DailyLogId == 0)
				{
					_context.StatusReports.Add(report);
				}
				else
				{
					_context.StatusReports.Update(report);
				}
				_context.SaveChanges();
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ViewBag.Action = (report.DailyLogId == 0) ? "Add" : "Edit";
				return View(report);
			}
		}

		[HttpGet]
		public IActionResult View(int id)
		{
			var log = _context.DailyLogs.Find(id);
			ViewBag.Date = log.Date;
			var reports = _context.StatusReports.Where(report => report.DailyLogId == log.DailyLogId).ToList();
			return View(reports);
		}
	}
}
