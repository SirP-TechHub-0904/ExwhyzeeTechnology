using ExwhyzeeTechnology.Infrastructure.TenantSupport;

namespace ExwhyzeeTechnology.Persistence.EF.SQL.Infrastructure;

public class MigrationsTenantProvider : ITenantProvider
{
    public string? CurrentTenant => null;

    public string DbSchemaName => "dbo"; 

    public string ConnectionString => "Persist Security Info=True;Integrated Security=true;Server=JSS\\SQLEXPRESS;Database=Exwhyzeetechdb;";

    public IDisposable BeginScope(string tenant)
    {
        throw new NotImplementedException();
    }
}
