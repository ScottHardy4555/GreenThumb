namespace GreenThumb.Models.DomainModels
{
	public class AddEditTicketVm
	{
		public List<Technician> Technicians { get; set; } = null!;
		public Ticket Ticket { get; set; } = null!;
		public string Operation { get; set; } = string.Empty;
		public List<string> TicketTypes { get; } = new List<string>
		{
			"Website",
			"Plants",
			"Electronics"
		};
	}
}
