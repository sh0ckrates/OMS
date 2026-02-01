using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OMS.Infrastructure.Data
{
    /// <summary>
    /// Used by EF Core tools (e.g. dotnet ef database update) at design time.
    /// Ensures the database file is created under the Infrastructure project folder.
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Resolve solution root (folder that contains OMS.Infrastructure), then put db in Infrastructure.
            // Works whether EF runs with startup-project OMS or from Infrastructure.
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null)
            {
                var infraDir = Path.Combine(dir.FullName, "OMS.Infrastructure");
                if (Directory.Exists(infraDir))
                {
                    var dbPath = Path.Combine(infraDir, "oms.db");
                    var connectionString = $"Data Source={dbPath};Journal Mode=Delete";
                    var options = new DbContextOptionsBuilder<AppDbContext>()
                        .UseSqlite(connectionString)
                        .Options;
                    return new AppDbContext(options);
                }
                dir = dir.Parent;
            }
            // Fallback: same folder as executing assembly
            var fallbackPath = Path.Combine(AppContext.BaseDirectory, "oms.db");
            var fallbackConnection = $"Data Source={fallbackPath};Journal Mode=Delete";
            return new AppDbContext(
                new DbContextOptionsBuilder<AppDbContext>().UseSqlite(fallbackConnection).Options);
        }
    }
}
