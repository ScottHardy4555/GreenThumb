﻿using System.ComponentModel.DataAnnotations;
namespace GreenThumb.Models.DomainModels
{
    public class Ticket
    {
        public int TicketId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "A technician is required")]
        public int? TechnicianId { get; set; } = null!;
        public Technician Technician { get; set; } = null!;

        [Required(ErrorMessage = "A type is required")]
        public string TypeName { get; set; } = string.Empty;

        public DateOnly? DateOpened { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public DateOnly? DateClosed { get; set; } = null!;
    }
}