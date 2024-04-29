using GreenThumb.Models;
using GreenThumb.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
namespace GreenThumb.Data.Services
{
    public class ContactService : IContactService
    {
        private ProjectContext _context { get; set; }
        public ContactService(ProjectContext ctx)
        {
            _context = ctx;
        }

        public List<Contact> GetAll() => _context.Contacts.ToList();

        public async Task<Contact> GetById(int id)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(t => t.ContactId == id);
            return contact ?? new Contact();
        }

        public async Task Add(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Contact contact)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChanges() => await _context.SaveChangesAsync();

    }
}
