using GreenThumb.Models;
using Microsoft.AspNetCore.Mvc;

namespace GreenThumb.Controllers
{
	public class ContactController : Controller
	{
		private ProjectContext _context { get; set; }
		public ContactController(ProjectContext ctx)
		{
			_context = ctx;
		}

		//View Delete Log Page
		[HttpGet]
		public IActionResult Delete(int id)
		{
			var contact = _context.Contacts.Find(id);
			return View(contact);
		}

		//Delete Log by Id
		[HttpPost]
		public IActionResult Delete(Contact contact)
		{
			_context.Contacts.Remove(contact);
			_context.SaveChanges();
			return RedirectToAction("List", "Contact");
		}

		[HttpGet]
		public IActionResult Add()
		{
			ViewBag.Action = "Add new daily contact";
			return View("Edit", new Contact());
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			ViewBag.Action = "Edit contact";
			var contact = _context.Contacts.Find(id);
			return View(contact);
		}

		[HttpPost]
		public IActionResult Edit(Contact contact)
		{
			if (ModelState.IsValid)
			{
				if (contact.ContactId == 0)
				{
					_context.Contacts.Add(contact);
					_context.SaveChanges();
					return RedirectToAction("List");
				}
				else
				{
					_context.Contacts.Update(contact);
					_context.SaveChanges();
					return RedirectToAction("View", "StatusReport", new { id = contact.ContactId });
				}
			}
			else
			{
				ViewBag.Action = (contact.ContactId == 0) ? "Add" : "Edit";
				return View(contact);
			}
		}

		[HttpGet]
		public IActionResult List()
		{
			ViewBag.Action = "View contacts";
			var dailyLogs = _context.Contacts.ToList();
			return View(dailyLogs);
		}

		[HttpGet]
		public IActionResult View(int id) 
		{
			
		}
	}
}
