using GreenThumb.Models;
using GreenThumb.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
namespace GreenThumb.Data.Services
{
    public class TechnicianService : ITechnicianService
	{
		private ProjectContext _context { get; set; }
		public TechnicianService(ProjectContext ctx)
		{
			_context = ctx;
		}

		public List<Technician> GetAll() => _context.Technicians.ToList();

		public async Task<Technician> GetById(int id)
		{
			var technician = await _context.Technicians.FirstOrDefaultAsync(t => t.TechnicianId == id);
			return technician ?? new Technician();
		}

		public async Task Add(Technician technician)
		{
			_context.Technicians.Add(technician);
			await _context.SaveChangesAsync();
		}

		public async Task Update(Technician technician)
		{
			_context.Technicians.Update(technician);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(Technician technician)
		{
			_context.Technicians.Remove(technician);
			await _context.SaveChangesAsync();
		}

		public async Task SaveChanges() => await _context.SaveChangesAsync();

	}
}
