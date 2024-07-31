using System.Net;
using System.Reflection.Metadata;
using ExwhyzeeTechnology.Application.Dtos;
using ExwhyzeeTechnology.Application.Repository.DomainServices;
using ExwhyzeeTechnology.Infrastructure.TenantSupport;

namespace ExwhyzeeTechnology.Website.Middlewares;
//https://github.com/Oriflame/ExwhyzeeTechnology#multiple-databases---complete-data-isolation
public class TenantScopeMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IDomainRepository _domainRepository;

    public TenantScopeMiddleware(RequestDelegate next, IDomainRepository domainRepository)
    {
        _next = next;
        _domainRepository = domainRepository;
    }

    public async Task Invoke(HttpContext httpContext, ITenantProvider tenantProvider)
    {

        // Got htis code from  http://blog.gaxion.com/2017/05/how-to-implement-multi-tenancy-with.html
        var GetAddress = httpContext.Request.Headers["Host"];

        var tenant = GetAddress[0];
        tenant = tenant.Replace("www.", "");
        tenant = tenant.Replace("https://", "");
        //, , , 
        //List<DomainListDto> domains = await _domainRepository.GetAllDomains();

        if (IPAddress.TryParse(tenant, out IPAddress ipAddress))
        {
            // It's an IP address, handle it accordingly
            using var scope = tenantProvider.BeginScope(tenant);
            await _next(httpContext);
            return;
        }
        else
        {


            if (tenant.ToString().ToLower() == "localhost:7543")
            {
                using var scope = tenantProvider.BeginScope(tenant);
                await _next(httpContext);

                return;

            }
            else if (tenant.ToString().ToLower() == "localhost:8096")
            {
                using var scope = tenantProvider.BeginScope(tenant);
                await _next(httpContext);

                return;

            }
           
            else if (tenant.ToString().ToLower() == "exwhyzee.ng")
            {
                using var scope = tenantProvider.BeginScope(tenant);
                await _next(httpContext);

                return;
            }
            
        }

        await _next(httpContext);
    }
}

/// <summary>
/// Extension method used to add the middleware to the HTTP request pipeline.
/// </summary>
public static class TenantScopeMiddlewareExtensions
{
    public static IApplicationBuilder UseTenantScopeMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TenantScopeMiddleware>();
    }
}
