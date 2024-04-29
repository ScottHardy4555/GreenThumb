using GreenThumb.Models.DomainModels;
namespace GreenThumb.Data.Services
{
    public interface ITechnicianService
	{
		List<Technician> GetAll();
		Task<Technician> GetById(int id);
		Task Add(Technician technician);
		Task Update(Technician technician);
		Task Delete(Technician technician);
		Task SaveChanges();
	}
}
