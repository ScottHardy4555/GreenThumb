using TicketM = GreenThumb.Models.DomainModels.Ticket;
using TechnicianM = GreenThumb.Models.DomainModels.Technician;
using GreenThumb.Data.Services;
using GreenThumb.Models.DataLayer;
using GreenThumb.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using GreenThumb.Utilities;
using Microsoft.AspNetCore.Authorization;
using GreenThumb.Models;

namespace GreenThumb.Areas.Ticket.Controllers
{
    [Area("Ticket")]
    public class TicketController : Controller
	{
		private Repository<TicketM> Tickets { get; set; }
		private Repository<TechnicianM> Technicians { get; set; }
		public TicketController(ProjectContext ctx)
		{
			Tickets = new Repository<TicketM>(ctx);
			Technicians = new Repository<TechnicianM>(ctx);
		}

		[Route("/tickets")]
		[HttpGet]
		public IActionResult List()
		{
			MySession session = new MySession(HttpContext.Session);
			string technicianId = session.GetTechnicianId();
			TicketManagerVM ticketManagerVM = new TicketManagerVM();
			if (technicianId != string.Empty)
			{
				int tId = int.Parse(technicianId);
				if (tId > 0)
				{
					ticketManagerVM.Tickets = Tickets.List().Where(t => t.TechnicianId == tId).ToList();
					ViewBag.CurrentTechnician = Technicians.Get(tId);
				}
				else if (tId == -1)
				{
					ticketManagerVM.Tickets = Tickets.List().Where(t => t.TechnicianId == null).ToList();
					ViewBag.FilterSelected = "unassigned";
				}
				else if (tId == -2)
				{
					ticketManagerVM.Tickets = Tickets.List().Where(t => t.DateClosed == null).ToList();
					ViewBag.FilterSelected = "open";
				}
				else
				{
					ticketManagerVM.Tickets = Tickets.List().ToList();
					ViewBag.FilterSelected = "all";
				}
			}
			else
			{
				ticketManagerVM.Tickets = Tickets.List().ToList();
			}
			ticketManagerVM.Technicians = Technicians.List().ToList();

			ViewBag.Action = "View tickets";



			return View(ticketManagerVM);
		}

		[HttpGet]
		public IActionResult Filter(string id)
		{
			MySession session = new MySession(HttpContext.Session);
			session.SetTechnicianId(id);
			return RedirectToAction("List");
		}

		public IActionResult View(int id)
		{
			ViewBag.Action = "View ticket";
			TicketM ticket = Tickets.Get(id);
			ViewBag.Technician = Technicians.Get(ticket.TechnicianId ?? 0);
			return View(ticket);
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add new daily ticket";

			AddEditTicketVm vm = new AddEditTicketVm { Ticket = new TicketM(), Technicians = Technicians.List().ToList() };
            return View("Edit", vm);
        }

		[Authorize(Roles = "Admin")]
		[HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit ticket";

			AddEditTicketVm vm = new AddEditTicketVm()
			{
				Ticket = Tickets.Get(id),
				Technicians = Technicians.List().ToList()
			};
            return View(vm);
        }

		[Authorize(Roles = "Admin")]
		[HttpPost]
        public IActionResult Edit(AddEditTicketVm addEditVm)
		{
			TicketM ticket = addEditVm.Ticket;
			if (ticket.Title != string.Empty &&
				ticket.Title != null &&
				ticket.Description != string.Empty &&
				ticket.Description != null &&
				ticket.TypeName != string.Empty &&
				ticket.TypeName != null)
            {
                if (ticket.TicketId == 0)
                {
					Tickets.Insert(ticket);
					Tickets.Save();
					SetStatusMessage($"{ticket.Title} was added.");
					return RedirectToAction("List");
                }
                else
                {
					Tickets.Update(ticket);
					Tickets.Save();
					SetStatusMessage($"{ticket.Title} was updated.");
					return RedirectToAction("List");
                }
            }
            else
            {
                ViewBag.Action = ticket.TicketId == 0 ? "Add" : "Edit";
                ViewBag.Technicians = Tickets.List().ToList();
				addEditVm.Technicians = Technicians.List().ToList();
                return View(addEditVm);
            }
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public IActionResult Delete(int id)
		{
			var ticket = Tickets.Get(id);
			return View(ticket);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Delete(TicketM ticket)
		{
			Tickets.Delete(ticket);
			Tickets.Save();
			SetStatusMessage($"{ticket.Title} was deleted.");
			return RedirectToAction("List");
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public IActionResult CloseTicket(int id)
		{
			TicketM? ticket = Tickets.Get(id);
            if (ticket != null)
			{
				ticket.DateClosed = DateOnly.FromDateTime(DateTime.Now);
				Tickets.Update(ticket);
				Tickets.Save();
				SetStatusMessage($"Ticket {ticket.Title} closed");
			}
			return RedirectToAction("List");
        }

		private TicketViewModel? ConvertToVm(TicketM? ticket)
        {
            if (ticket != null)
            {
                TicketViewModel ticketVm = new TicketViewModel()
                {
                    TicketId = ticket.TicketId,
                    Title = ticket.Title,
                    Description = ticket.Description,
                    TechnicianId = ticket.TechnicianId,
                    TypeName = ticket.TypeName,
                    DateOpened = ticket.DateOpened,
                    DateClosed = ticket.DateClosed,
                };
                return ticketVm;
            }
            return null;
        }

		private void SetStatusMessage(string message)
		{
			TempData["StatusMessage"] = message;
		}
	}
}
