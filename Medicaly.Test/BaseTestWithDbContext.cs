using Medicaly.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Medicaly.Test;

public abstract class BaseTestWithDbContext
{
    protected readonly MedicalyDbContext DbContext;
    
    protected BaseTestWithDbContext()
    {
        var options = new DbContextOptionsBuilder<MedicalyDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;
        DbContext = new MedicalyDbContext(options, false);
        DbContext.Database.OpenConnection();
        DbContext.Database.EnsureCreated();
        
    }
}