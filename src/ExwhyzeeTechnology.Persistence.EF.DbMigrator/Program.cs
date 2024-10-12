using System.Reflection;
using ExwhyzeeTechnology.Infrastructure.Extensions;
using ExwhyzeeTechnology.Infrastructure.TenantSupport;
using ExwhyzeeTechnology.Persistence.EF.DbMigrator;
using ExwhyzeeTechnology.Persistence.EF.SQL;
using ExwhyzeeTechnology.Persistence.EF.SQL.Extensions;
using Microsoft.Extensions.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(configBuilder =>
    {
        // need to set base path to make it work with shared appsettings files
        configBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location));
    })
    .ConfigureServices((hostContext, services) =>
    {

        // Register IHttpContextAccessor
        services.AddHttpContextAccessor();


        // services.AddTenantSupport(hostContext.Configuration);
        services.AddATenantSupport(hostContext.Configuration);
        services.AddEntityFrameworkSqlServer<DashboardDbContext>();
        services.AddHostedService<DbMigratorHostedService>();
 
    })
    .Build();

await host.RunAsync();
