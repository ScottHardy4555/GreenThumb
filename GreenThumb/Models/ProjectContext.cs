using GreenThumb.Models.Configuration;
using GreenThumb.Models.DomainModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace GreenThumb.Models
{
    public class ProjectContext : IdentityDbContext<User>
    {
        public DbSet<DailyLog> DailyLogs { get; set; } = null!;
        public DbSet<StatusReport> StatusReports { get; set; } = null!;
        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<Technician> Technicians { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;

        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ContactConfig());
            builder.ApplyConfiguration(new DailyLogConfig());
            builder.ApplyConfiguration(new StatusReportConfig());
            builder.ApplyConfiguration(new TechnicianConfig());
            builder.ApplyConfiguration(new TicketConfig());
        }
    }
}
