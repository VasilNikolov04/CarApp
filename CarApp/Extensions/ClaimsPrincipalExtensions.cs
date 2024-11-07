using System.Security.Claims;

namespace CarApp.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetUserId(this ClaimsPrincipal? userClaimsPrincipal)
        {
            return userClaimsPrincipal?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
