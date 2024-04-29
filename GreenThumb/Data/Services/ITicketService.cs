using GreenThumb.Models.DomainModels;
namespace GreenThumb.Data.Services
{
    public interface ITicketService
	{
		List<Ticket> GetAll();
		Task<Ticket> GetById(int id);
		Task Add(Ticket ticket);
		Task Update(Ticket ticket);
		Task Delete(Ticket ticket);
		Task SaveChanges();
	}
}
