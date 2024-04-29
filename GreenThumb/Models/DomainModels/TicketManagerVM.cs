namespace GreenThumb.Models.DomainModels
{
	public class TicketManagerVM
	{
		public List<Ticket> Tickets { get; set; } = null!;
		public List<Technician> Technicians { get; set; } = null!;
		public string Display { get; set; } = string.Empty;
	}
}
