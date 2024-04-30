using GreenThumb.Data.Services;
using GreenThumb.Models.DataLayer;
using Microsoft.AspNetCore.Mvc;
using ContactM = GreenThumb.Models.DomainModels.Contact;
using NuGet.Protocol.Core.Types;
using Microsoft.AspNetCore.Authorization;
using GreenThumb.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenThumb.Areas.Contact.Controllers
{
    [Area("Contact")]
    public class ContactController : Controller
    {
        private Repository<ContactM> Contacts { get; set; }
        public ContactController(ProjectContext ctx) => Contacts = new Repository<ContactM>(ctx);

		[Route("/contacts")]
		[HttpGet]
		public IActionResult List(string id = "All")
		{
			ViewBag.Action = "View contacts";

			var contactOptions = new QueryOptions<ContactM>(){ OrderBy = c => c.ContactId };

			List<ContactM> contacts = Contacts.List(contactOptions).ToList();
			return View(contacts);
		}

		[HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add new daily contact";
            return View("Edit", new ContactM());
        }

		[Authorize(Roles = "Admin")]
		[HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit contact";
            var contact = Contacts.Get(id);
            return View(contact);
        }

		[Authorize(Roles = "Admin")]
		[HttpPost]
        public IActionResult Edit(ContactM contact)
        {
            if (ModelState.IsValid)
            {
                if (EmailExists(contact))
                {
					ModelState.AddModelError("Email", "Email already exists");
					ViewBag.Action = contact.ContactId == 0 ? "Add" : "Edit";
					return View(contact);
				}
                if (contact.ContactId == 0)
                {
					Contacts.Insert(contact);
                    Contacts.Save();
                    SetStatusMessage($"{contact.FirstName} {contact.LastName} was added to contacts.");
                    return RedirectToAction("List");
                }
                else
                {
					Contacts.Update(contact);
                    Contacts.Save();
					SetStatusMessage($"{contact.FirstName} {contact.LastName} was updated.");
					return RedirectToAction("List", new { id = contact.ContactId });
                }
            }
            else
            {
                ViewBag.Action = contact.ContactId == 0 ? "Add" : "Edit";
                return View(contact);
            }
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public IActionResult Delete(int id)
		{
			var contact = Contacts.Get(id);
			return View(contact);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Delete(ContactM contact)
		{
			Contacts.Delete(contact);
			Contacts.Save();
			SetStatusMessage($"{contact.FirstName} {contact.LastName} was removed.");
			return RedirectToAction("List", "Contact");
		}

        private void SetStatusMessage(string message)
        {
            TempData["StatusMessage"] = message;
		}

		public bool EmailExists(ContactM contact) => Contacts.List().Any(c => c.Email == contact.Email);
	}
}
