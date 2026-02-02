using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OMS.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null)
            {
                var infraDir = Path.Combine(dir.FullName, "OMS.Infrastructure");
                if (Directory.Exists(infraDir))
                {
                    var dbPath = Path.Combine(infraDir, "oms.db");
                    var connectionString = $"Data Source={dbPath}";
                    var options = new DbContextOptionsBuilder<AppDbContext>()
                        .UseSqlite(connectionString)
                        .Options;
                    return new AppDbContext(options);
                }
                dir = dir.Parent;
            }
            var fallbackPath = Path.Combine(AppContext.BaseDirectory, "oms.db");
            var fallbackConnection = $"Data Source={fallbackPath}";
            return new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlite(fallbackConnection).Options);
        }
    }
}
