using Microsoft.EntityFrameworkCore;

namespace PalTracker
{
    public class TimeEntryContext : DbContext
    {
        public TimeEntryContext(DbContextOptions dbContextOpts) : base(dbContextOpts)
        {

        }

        public DbSet<TimeEntryRecord> TimeEntryRecords { get; set; }
    }
}
