using System.Security.Claims;

namespace BlazorApp1.Authentication
{
    public record UserSession(string Name, string Email, string Role);

    public static class UserSessionEx
    {
        public static ClaimsIdentity GetClaimsIdentity(this UserSession userSession, string? authType = null)
        {
            return new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.Name),
                    new Claim(ClaimTypes.Email, userSession.Email),
                    new Claim(ClaimTypes.Role, userSession.Role)
                }, authType);
        }

        public static UserSession FromUserAccount(UserAccount userAccount)
        {
            return new UserSession(userAccount.Name, userAccount.Email,userAccount.Role);
        }
    }
}