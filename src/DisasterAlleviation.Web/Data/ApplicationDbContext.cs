using DisasterAlleviation.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviation.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}

        public DbSet<Incident> Incidents => Set<Incident>();
        public DbSet<Donation> Donations => Set<Donation>();
        public DbSet<VolunteerTask> VolunteerTasks => Set<VolunteerTask>();
        public DbSet<VolunteerAssignment> VolunteerAssignments => Set<VolunteerAssignment>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<VolunteerAssignment>()
                .HasIndex(v => new { v.TaskId, v.UserId })
                .IsUnique();
        }
    }
}
