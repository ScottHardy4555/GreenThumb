using GreenThumb.Models;
using GreenThumb.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
namespace GreenThumb.Data.Services
{
    public class StatusReportService : IStatusReportService
	{
		private ProjectContext _context { get; set; }
		public StatusReportService(ProjectContext ctx)
		{
			_context = ctx;
		}

		public List<StatusReport> GetAll() => _context.StatusReports.ToList();

		public async Task<StatusReport> GetById(int id)
		{
			var statusReport = await _context.StatusReports.FirstOrDefaultAsync(t => t.StatusReportId == id);
			return statusReport ?? new StatusReport();
		}

		public async Task Add(StatusReport statusReport)
		{
			_context.StatusReports.Add(statusReport);
			await _context.SaveChangesAsync();
		}

		public async Task Update(StatusReport statusReport)
		{
			_context.StatusReports.Update(statusReport);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(StatusReport statusReport)
		{
			_context.StatusReports.Remove(statusReport);
			await _context.SaveChangesAsync();
		}

		public async Task SaveChanges() => await _context.SaveChangesAsync();

	}
}
