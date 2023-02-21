using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TebanRM.Domain.Entities;
using TebanRM.Domain.Persistence.Configuration;

namespace TebanRM.Domain.Persistence;
public class ApplicationDbContext : IdentityDbContext<TebanUser>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<CommunicationSchedule> CommunicationSchedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }
}