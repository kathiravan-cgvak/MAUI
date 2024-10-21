namespace Task4
{
    using Microsoft.AspNetCore.Components.Authorization;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.JSInterop;

    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var isAuthenticated = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "isAuthenticated");
            if (isAuthenticated == "true")
            {
                // Simulating a hardcoded user.
                var identity = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, "TestUser")
            }, "testAuthType");

                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }
            return new AuthenticationState(_anonymous);
        }

        public void MarkUserAsAuthenticated()
        {
            var identity = new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.Name, "TestUser")
        }, "testAuthType");

            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void MarkUserAsLoggedOut()
        {
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }
    }
}