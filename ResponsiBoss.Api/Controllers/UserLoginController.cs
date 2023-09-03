using Microsoft.AspNetCore.Mvc;
using ResponsiBoss.Api.Models;

namespace ResponsiBoss.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserLoginController : ControllerBase
    {
        private readonly ILogger<UserLoginController> _logger;

        public UserLoginController(ILogger<UserLoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Login", Name = "UserLogin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<UserLoginModel> Get([FromBody] UserLoginModel userLoginModel)
        {
            return null;
        }
    }
}