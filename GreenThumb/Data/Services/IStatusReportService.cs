using GreenThumb.Models.DomainModels;
namespace GreenThumb.Data.Services
{
    public interface IStatusReportService
	{
		List<StatusReport> GetAll();
		Task<StatusReport> GetById(int id);
		Task Add(StatusReport statusReport);
		Task Update(StatusReport statusReport);
		Task Delete(StatusReport statusReport);
		Task SaveChanges();
	}
}
