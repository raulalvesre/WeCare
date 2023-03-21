using System.Security.Claims;

namespace WeCare.Application.Interfaces;

public interface ICurrentUser
{
    long GetUserId();
    string GetUserEmail();
    string GetUserRole();
    bool IsInRole(string role);
    IEnumerable<Claim> GetClaimsIdentity();
}