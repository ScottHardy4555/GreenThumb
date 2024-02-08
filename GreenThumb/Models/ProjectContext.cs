using Microsoft.EntityFrameworkCore;
namespace GreenThumb.Models
{
    public class ProjectContext : DbContext
    {
        public DbSet<DailyLog> DailyLogs { get; set; } = null!;
        public DbSet<StatusReport> StatusReports { get; set; } = null!;
        public DbSet<Contact> Contacts { get; set; } = null!;
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyLog>().HasData(
                new DailyLog() { DailyLogId = 1, Date = new DateOnly(2024, 1, 31) }
            );

            modelBuilder.Entity<StatusReport>().HasData(
                new StatusReport() { StatusReportId = 1, DailyLogId = 1, Time = new TimeOnly(0, 0), Temperature = 29f, Humidity = 60f, PartsPerMillion = 10f, ElectricalConductivity = 128f, PH = 7.2f },
                new StatusReport() { StatusReportId = 2, DailyLogId = 1, Time = new TimeOnly(1, 0), Temperature = 22.5f, Humidity = 62f, PartsPerMillion = 10f, ElectricalConductivity = 128f, PH = 7.1f },
                new StatusReport() { StatusReportId = 3, DailyLogId = 1, Time = new TimeOnly(2, 0), Temperature = 20.1f, Humidity = 64.5f, PartsPerMillion = 10f, ElectricalConductivity = 128f, PH = 7.2f }
            );

            /*modelBuilder.Entity<Contact>().HasData(
                new Contact() { ContactId = 1, }
            );*/
        }
    }
}
