using ExwhyzeeTechnology.Persistence.EF.SQL.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExwhyzeeTechnology.Persistence.EF.SQL;

public class MigrationsDbContextFactory : IDesignTimeDbContextFactory<DashboardDbContext>
{
    public DashboardDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<DashboardDbContext>();
        // You need to provide an instance of IHttpContextAccessor and ITenantProvider
        var httpContextAccessor = new HttpContextAccessor(); // Example, modify as needed
        return new DashboardDbContext(builder.Options, new MigrationsTenantProvider(), httpContextAccessor);
    }
}
