 using Microsoft.AspNetCore.Components.Authorization;
using OrganizeDotNET6a.Shared.Contracts;
using OrganizeDotNET6a.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.WASM.OrganizeAuthenticationStateProvider
{
    public class SimpleAuthenticationStateProvider : AuthenticationStateProvider, IAuthenticationStateProvider
    {
        private readonly ICurrentUserService _currentUserService;

        public SimpleAuthenticationStateProvider(
            ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            if (_currentUserService.CurrentUser == null)
            {
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }

            var authenticatedUser = new ClaimsPrincipal(
                new ClaimsIdentity(new[] { 
                    new Claim("id", _currentUserService.CurrentUser.Id.ToString()),
                    new Claim(ClaimTypes.Role,"admin")}
                , "apiauth"));
            return Task.FromResult(new AuthenticationState(authenticatedUser));
        }

        public void SetAuthenticatedState(clsUser objUser)
        {
             var authenticatedUser = new ClaimsPrincipal(
                new ClaimsIdentity(new[] { 
                    new Claim("id", objUser.Id.ToString()), 
                    new Claim(ClaimTypes.Role, "admin") }, 
                    "apiauth"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void UnsetUser()
        {
            var unsetUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(unsetUser));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
