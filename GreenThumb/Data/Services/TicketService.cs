using GreenThumb.Models;
using GreenThumb.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
namespace GreenThumb.Data.Services
{
    public class TicketService : ITicketService
	{
		private ProjectContext _context { get; set; }
		public TicketService(ProjectContext ctx)
		{
			_context = ctx;
		}

		public List<Ticket> GetAll() => _context.Tickets.ToList();

		public async Task<Ticket> GetById(int id)
		{
			var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.TicketId == id);
			return ticket ?? new Ticket();
		}

		public async Task Add(Ticket ticket)
		{
			_context.Tickets.Add(ticket);
			await _context.SaveChangesAsync();
		}

		public async Task Update(Ticket ticket)
		{
			_context.Tickets.Update(ticket);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(Ticket ticket)
		{
			_context.Tickets.Remove(ticket);
			await _context.SaveChangesAsync();
		}

		public async Task SaveChanges() => await _context.SaveChangesAsync();

	}
}
