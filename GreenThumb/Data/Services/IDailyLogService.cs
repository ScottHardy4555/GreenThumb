using GreenThumb.Models.DomainModels;
namespace GreenThumb.Data.Services
{
    public interface IDailyLogService
	{
		List<DailyLog> GetAll();
		Task<DailyLog> GetById(int id);
		Task Add(DailyLog dailyLog);
		Task Update(DailyLog dailyLog);
		Task Delete(DailyLog dailyLog);
		Task SaveChanges();
	}
}
