using System.Security.Claims;
using WeCare.Application.Interfaces;

namespace WeCare.API.Extensions;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _accessor;

    public CurrentUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    private bool IsAuthenticated()
    {
        return _accessor.HttpContext.User.Identity.IsAuthenticated;
    }

    public long GetUserId()
    {
        return IsAuthenticated() ? _accessor.HttpContext.User.GetUserId() : 0;
    }

    public string GetUserEmail()
    {
        return IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : "";
    }

    public string GetUserRole()
    {
        return IsAuthenticated() ? _accessor.HttpContext.User.GetUserRole() : "";
    }

    public bool IsInRole(string role)
    {
        return _accessor.HttpContext.User.IsInRole(role);
    }

    public IEnumerable<Claim> GetClaimsIdentity()
    {
        return _accessor.HttpContext.User.Claims;
    }
}

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentException(nameof(principal));

        var claim = principal.FindFirst("Id");
        return int.Parse(claim?.Value);
    }

    public static string GetUserEmail(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentException(nameof(principal));

        var claim = principal.FindFirst(ClaimTypes.Email);
        return claim?.Value;
    }

    public static string GetUserRole(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentException(nameof(principal));

        var claim = principal.FindFirst(ClaimTypes.Role);
        return claim?.Value;
    }
}