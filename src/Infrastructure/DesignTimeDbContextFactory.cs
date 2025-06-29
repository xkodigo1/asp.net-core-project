using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Infrastructure.Persistence;

namespace Infrastructure;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));
        var connectionString = "Server=localhost;Port=3306;Database=AutoTallerManager;User=root;Password=kodigoDev0.";

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseMySql(connectionString, serverVersion,
            mysqlOptions => mysqlOptions.EnableRetryOnFailure());

        return new ApplicationDbContext(optionsBuilder.Options);
    }
} 