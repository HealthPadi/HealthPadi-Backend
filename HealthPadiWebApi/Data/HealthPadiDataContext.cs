using HealthPadiWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthPadiWebApi.Data
{ 
   public class HealthPadiDataContext : IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    { 
    public HealthPadiDataContext(DbContextOptions<HealthPadiDataContext> options) : base(options)
    {
    }

    public DbSet<Report> Reports { get; set; }
    public DbSet<HealthUpdate> HealthUpdates { get; set; }
    public DbSet<Feed> Feeds { get; set; }
    public DbSet<TaskExecutionLog> TaskExecutionLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // This line ensures that the default Identity configurations are applied
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Report>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reports)
            .HasForeignKey(u => u.UserId)
            //Prevents deletion of User when Report is deleted 
            .OnDelete(DeleteBehavior.Restrict);
    }
}
}
