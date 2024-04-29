using GreenThumb.Models;
using GreenThumb.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace GreenThumb.Data.Services
{
    public class DailyLogService : IDailyLogService
	{
		private ProjectContext _context { get; set; }
		public DailyLogService(ProjectContext ctx)
		{
			_context = ctx;
		}

		public List<DailyLog> GetAll() => _context.DailyLogs.ToList();

		public async Task<DailyLog> GetById(int id)
		{
			var dailyLog = await _context.DailyLogs.FirstOrDefaultAsync(t => t.DailyLogId == id);
			return dailyLog ?? new DailyLog();
		}

		public async Task Add(DailyLog dailyLog)
		{
			_context.DailyLogs.Add(dailyLog);
			await _context.SaveChangesAsync();
		}

		public async Task Update(DailyLog dailyLog)
		{
			_context.DailyLogs.Update(dailyLog);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(DailyLog dailyLog)
		{
			_context.DailyLogs.Remove(dailyLog);
			await _context.SaveChangesAsync();
		}

		public async Task SaveChanges() => await _context.SaveChangesAsync();

	}
}
