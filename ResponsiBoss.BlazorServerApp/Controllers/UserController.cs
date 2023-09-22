using Microsoft.AspNetCore.Mvc;
using ResponsiBoss.BlazorServerApp.Identity;
using ResponsiBoss.BlazorServerApp.Identity.Abstractions;

namespace ResponsiBoss.BlazorServerApp.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IApplicationSignInManager _applicationSignInManager;
        private readonly CustomStorage _customStorage;

        public UserController(IApplicationSignInManager applicationSignInManager,
            CustomStorage customStorage)
        {
            _applicationSignInManager = applicationSignInManager;
            _customStorage = customStorage;
        }

        [HttpGet]
        public async Task<IActionResult> Login(Guid key)
        {
            var userSession = _customStorage.UserSession.ReadEncryptedItem<UserSession>(key);

            if(userSession == null)
                return Redirect("/Login");

            await _applicationSignInManager.SignInAsync(userSession.Email, userSession.Password);

            if (userSession != null)
                _customStorage.UserSession.Remove(key);

            return Redirect("/");
        }
    }
}