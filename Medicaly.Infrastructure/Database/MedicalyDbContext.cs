using Microsoft.EntityFrameworkCore;

namespace Medicaly.Infrastructure.Database;

public class MedicalyDbContext: DbContext
{
    public MedicalyDbContext()
    {
    }
    
    public MedicalyDbContext(
        DbContextOptions<MedicalyDbContext> options): base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("public");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseNpgsql();
        }
    }
}