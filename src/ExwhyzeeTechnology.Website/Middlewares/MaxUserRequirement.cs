using Microsoft.AspNetCore.Authorization;

namespace ExwhyzeeTechnology.Website.Middlewares
{
  public class MaxUserRequirement : IAuthorizationRequirement
{
    public int MaxUserCount { get; }

    public MaxUserRequirement(int maxUserCount)
    {
        MaxUserCount = maxUserCount;
    }
}
}
