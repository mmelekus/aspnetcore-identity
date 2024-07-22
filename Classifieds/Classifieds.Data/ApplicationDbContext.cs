using Classifieds.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Classifieds.Data;

public partial class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext() {}
    public ApplicationDbContext(DbContextOptions options) : base(options) {}

    public DbSet<Category> Categories { get; set; }
    public DbSet<Advertisement> Advertisements{ get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}