using Microsoft.JSInterop;
using System.Threading.Tasks;
namespace Task4
{
    

    public class AuthenticationService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly CustomAuthenticationStateProvider _authStateProvider;

        public AuthenticationService(IJSRuntime jsRuntime, CustomAuthenticationStateProvider authStateProvider)
        {
            _jsRuntime = jsRuntime;
            _authStateProvider = authStateProvider;
        }

        public async Task<bool> Login(string username, string password)
        {
            // Hardcoded credentials for simplicity
            if (username == "testuser" && password == "password")
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "isAuthenticated", "true");
                _authStateProvider.MarkUserAsAuthenticated();
                return true;
            }
            return false;
        }

        public async Task Logout()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "isAuthenticated", "false");
            _authStateProvider.MarkUserAsLoggedOut();
        }
    }

}
