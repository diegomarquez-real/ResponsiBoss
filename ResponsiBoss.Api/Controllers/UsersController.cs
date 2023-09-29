using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResponsiBoss.Api.Models;
using ResponsiBoss.Api.Models.Create;
using ResponsiBoss.Api.Models.Update;
using ResponsiBoss.Api.Services.Abstractions;
using System.Reflection.Metadata.Ecma335;

namespace ResponsiBoss.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        private readonly IUserAuthenticationService _userAuthenticationService;

        public UsersController(ILogger<UsersController> logger,
            IUserService userService,
            IUserAuthenticationService userAuthenticationService)
        {
            _logger = logger;
            _userService = userService;
            _userAuthenticationService = userAuthenticationService;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(typeof(AuthTokenModel), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(UnauthorizedResult), 401)]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginModel userLoginModel)
        {
            try
            {
                var result = await _userAuthenticationService.AuthenticateAsync(userLoginModel);

                if (result == null)
                    return Unauthorized();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Authenticate.");

                return BadRequest();
            }
        }

        [HttpGet("{id}", Name = "GetUser")]
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetUserAsync([FromRoute] Guid id)
        {
            try
            {
                var user = await _userService.FindByIdAsync(id);

                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get User.");

                return BadRequest();
            }
        }

        [HttpGet("GetAll", Name = "GetAllUsers")]
        [ProducesResponseType(typeof(List<UserModel>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        
        public async Task<IActionResult> GetUsersAsync()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get Users.");

                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost(Name = "CreateUser")]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserModel createUserModel)
        {
            try
            {
                var result = await _userService.CreateUserAsync(createUserModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Create User.");

                return BadRequest();
            }  
        }

        [HttpPut("{id}", Name = "UpdateUser")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid id, [FromBody] UpdateUserModel updateUserModel)
        {
            try
            {
                var user = await _userService.FindByIdAsync(id);

                if (user == null)
                    return NotFound();

                await _userService.UpdateUserAsync(user, updateUserModel);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Update User.");

                return BadRequest();
            }
        }

        [HttpDelete("{id}", Name = "DeleteUser")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Delete User.");

                return BadRequest();
            }
        }
    }
}