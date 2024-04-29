using StatusReportM = GreenThumb.Models.DomainModels.StatusReport;
using DailyLogM = GreenThumb.Models.DomainModels.DailyLog;
using GreenThumb.Models.DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GreenThumb.Models;
using GreenThumb.Models.DomainModels;

namespace GreenThumb.Areas.DailyLog.Controllers
{
    [Area("DailyLog")]
    public class StatusReportController : Controller
	{
		private Repository<StatusReportM> StatusReports { get; set; }
		private Repository<DailyLogM> DailyLogs { get; set; }
        public StatusReportController(ProjectContext ctx)
        {
            StatusReports = new Repository<StatusReportM>(ctx);
			DailyLogs = new Repository<DailyLogM>(ctx);
		}

		[Route("logs/delete/{id?}")]
		[Authorize(Roles = "Admin")]
		[HttpGet]
        public IActionResult Delete(int id)
        {
            StatusReportM? report = StatusReports.Get(id);
			StatusReports.Delete(report);
			StatusReports.Save();
			SetStatusMessage($"{report.Time.ToShortTimeString()} was deleted.");
			return RedirectToAction("View", "StatusReport", new {id = report.DailyLogId});
        }

        [Route("logs/delete/{id?}")]
		[Authorize(Roles = "Admin")]
		[HttpPost]
        public IActionResult Delete(StatusReportM report, int id)
        {
			return RedirectToAction("View", "StatusReport", new { id });
        }

		//[Route("logs/view/{dailyLogId?}/add")]
		[Authorize(Roles = "Admin")]
		[HttpGet]
        public IActionResult Add(int dailyLogId)
        {
            ViewBag.Action = "Add new hourly report";
            AddEditStatusReportVM report = new AddEditStatusReportVM();
            ViewBag.DailyLogId = dailyLogId;
			return View("Edit", report);
        }

		//[Route("logs/view/{dailyLogId?}/edit/{id?}")]
		[Authorize(Roles = "Admin")]
		[HttpGet]
        public IActionResult Edit(int id, int dailyLogId)
        {
            ViewBag.Action = "Edit log";
            StatusReportM? report = StatusReports.Get(id) ?? null;
            if (report == null)
            {
                return View("View", new { id = dailyLogId });
            }
            AddEditStatusReportVM reportVM = new AddEditStatusReportVM 
            {
				StatusReportId = report.StatusReportId,
				Time = report.Time,
				Temperature = report.Temperature,
				ElectricalConductivity = report.ElectricalConductivity,
				PartsPerMillion = report.PartsPerMillion,
				Humidity = report.Humidity,
				PH = report.PH,
			};
            ViewBag.DailyLogId = dailyLogId;
            return View(reportVM);
        }

		[Authorize(Roles = "Admin")]
		[HttpPost]
        public IActionResult Edit(AddEditStatusReportVM reportVM, int dailyLogId)
        {
            
            if (ModelState.IsValid)
            {
                StatusReportM report = new StatusReportM
                {
                    StatusReportId = reportVM.StatusReportId,
                    DailyLogId = dailyLogId,
                    Time = reportVM.Time,
                    Temperature = reportVM.Temperature,
                    ElectricalConductivity = reportVM.ElectricalConductivity,
                    PartsPerMillion = reportVM.PartsPerMillion,
                    Humidity = reportVM.Humidity,
                    PH = reportVM.PH,
                };
                report.DailyLogId = dailyLogId;
                if (report.StatusReportId == 0)
                {
					StatusReports.Insert(report);
                    StatusReports.Save();
					SetStatusMessage($"{report.Time.ToShortTimeString()} was added.");
				}
                else
                {
					StatusReports.Update(report);
					StatusReports.Save();
					SetStatusMessage($"{report.Time.ToShortTimeString()} was modified.");
				}
                return RedirectToAction("View", "StatusReport", new { id = dailyLogId });
            }
            else
            {
                ViewBag.Action = reportVM.StatusReportId == 0 ? "Add" : "Edit";
                return View(reportVM);
            }
        }

        //[Route("logs/view/{id?}")]
        [HttpGet]
        public IActionResult View(int id)
        {
            List<StatusReportM> reports = StatusReports.List().Where(s => s.DailyLogId == id).ToList();
            ViewBag.Log = DailyLogs.Get(id);
            return View(reports);
		}

		private void SetStatusMessage(string message)
		{
			TempData["StatusMessage"] = message;
		}
	}
}
