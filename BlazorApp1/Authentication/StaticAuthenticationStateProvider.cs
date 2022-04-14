using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace BlazorApp1.Authentication
{
    public class StaticAuthenticationStateProvider : AuthenticationStateProvider
    {
        private const string _userSessionName = "UserSession";
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymousPrincipal = new(new ClaimsIdentity());

        public StaticAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSessionStorageResult = await _sessionStorage.GetAsync<UserSession>(_userSessionName);
                var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;

                // session not found or corrupted
                if (userSession is null)
                {
                    return await Task.FromResult(new AuthenticationState(_anonymousPrincipal));
                }

                var claimsPrincipal = new ClaimsPrincipal(userSession.GetClaimsIdentity("CustomAuth"));

                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                // if anything goes wrong, just assume user is anonymous
                return await Task.FromResult(new AuthenticationState(_anonymousPrincipal));
            }
        }

        /// <summary>
        /// This method used to update auth state from our code
        /// </summary>
        /// <param name="userSession">User session. Pass null to perform logout.</param>
        public async Task UpdateAuthenticationState(UserSession? userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                await _sessionStorage.SetAsync(_userSessionName, userSession);

                claimsPrincipal = new ClaimsPrincipal(userSession.GetClaimsIdentity());
            } 
            else
            {
                await _sessionStorage.DeleteAsync(_userSessionName);

                claimsPrincipal = _anonymousPrincipal;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
