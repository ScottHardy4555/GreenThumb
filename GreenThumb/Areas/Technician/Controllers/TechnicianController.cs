using TechnicianM = GreenThumb.Models.DomainModels.Technician;
using GreenThumb.Models.DataLayer;
using Microsoft.AspNetCore.Mvc;
using GreenThumb.Models.DomainModels;
using GreenThumb.Utilities;
using Microsoft.AspNetCore.Authorization;
using GreenThumb.Models;

namespace GreenThumb.Areas.Technician.Controllers
{
    [Area("Technician")]
	public class TechnicianController : Controller
	{
		private Repository<TechnicianM> Technicians { get; set; }
		public TechnicianController(ProjectContext ctx) => Technicians = new Repository<TechnicianM>(ctx);

		[Authorize(Roles = "Admin")]
		[HttpGet]
        public IActionResult Delete(int id)
        {
            var technician = Technicians.Get(id);
            return View(technician);
        }

		[Authorize(Roles = "Admin")]
		[HttpPost]
        public IActionResult Delete(Models.DomainModels.Technician technician)
        {
			Technicians.Delete(technician);
            Technicians.Save();

			SetStatusMessage($"{technician.Name} was deleted.");
			return RedirectToAction("List", "Technician");
        }

		[Authorize(Roles = "Admin")]
		[HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add new daily technician";
            return View("Edit", new TechnicianM());
        }

		[Authorize(Roles = "Admin")]
		[HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit technician";
            var technician = Technicians.Get(id);
            return View(technician);
        }

		[Authorize(Roles = "Admin")]
		[HttpPost]
        public IActionResult Edit(TechnicianM technician)
        {
            if (ModelState.IsValid)
            {
                if (technician.TechnicianId == 0)
                {
					Technicians.Insert(technician);
                    Technicians.Save();
					SetStatusMessage($"{technician.Name} was added.");
					return RedirectToAction("List");
                }
                else
                {
					Technicians.Update(technician);
                    Technicians.Save();
					SetStatusMessage($"{technician.Name} was updated.");
					return RedirectToAction("List", new { id = technician.TechnicianId });
                }
            }
            else
            {
                ViewBag.Action = technician.TechnicianId == 0 ? "Add" : "Edit";
                return View(technician);
            }
        }

        [Route("/technicians")]
        [HttpGet]
        public IActionResult List()
        {
            ViewBag.Action = "View technicians";

            QueryOptions<TechnicianM> technicianOptions = new QueryOptions<TechnicianM>()
            {
                OrderBy = t => t.Name
            };

            var technicians = Technicians.List(technicianOptions);
            return View(technicians);
		}

        [HttpGet]
        public IActionResult Get()
		{
			ViewBag.Technicians = Technicians.List().ToList();
			return View(new GetTechnicianVM());
        }

        [HttpPost]
        public IActionResult Get(GetTechnicianVM getTechnicianVM)
        {
            string technicianId = getTechnicianVM.TechnicianId;

			if (technicianId == string.Empty || technicianId == null)
            {
                ViewBag.Technicians = Technicians.List().ToList();
                ModelState.AddModelError("TechnicianId", "Please select a technician");
                return View(new GetTechnicianVM());
            }
            MySession session = new MySession(HttpContext.Session);
            session.SetTechnicianId(technicianId);
            return RedirectToAction("List", "Ticket", new { area = "Ticket" });
        }

		private void SetStatusMessage(string message)
		{
			TempData["StatusMessage"] = message;
		}
	}
}
