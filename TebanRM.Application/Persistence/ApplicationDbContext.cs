using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TebanRM.Application.Models;
using TebanRM.Application.Persistence.Configuration;

namespace TebanRM.Application.Persistence;
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