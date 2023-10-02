using Microsoft.AspNetCore.Mvc;
using ResponsiBoss.Api.Models;
using ResponsiBoss.BlazorServerApp.Identity;
using ResponsiBoss.BlazorServerApp.Identity.Abstractions;

namespace ResponsiBoss.BlazorServerApp.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IApplicationSignInManager _applicationSignInManager;
        private readonly IApplicationSignOutManager _applicationSignOutManager;
        private readonly CustomStorage _customStorage;

        public UserController(IApplicationSignInManager applicationSignInManager,
            IApplicationSignOutManager applicationSignOutManager,
            CustomStorage customStorage)
        {
            _applicationSignInManager = applicationSignInManager;
            _applicationSignOutManager = applicationSignOutManager;
            _customStorage = customStorage;
        }

        [HttpGet]
        public async Task<IActionResult> LoginAsync(Guid key)
        {
            var authTokenModel = _customStorage.UserSession.ReadEncryptedItem<AuthTokenModel>(key);

            if(authTokenModel == null)
                return Redirect("/Login");

            await _applicationSignInManager.SignInAsync(authTokenModel);

            _customStorage.UserSession.Remove(key);

            return Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> LogoutAsync()
        {
            await _applicationSignOutManager.SignOutAsync();

            return Redirect("/Logout");
        }
    }
}