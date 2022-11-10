using DatingApp.BLL.DTO;
using DatingApp.BLL.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Controllers
{
    [Route("[controller]/")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUserDto>>> GetUsers()
        {
            var users = await _userService.GetAllAppUsers();

            if (!users.Success)
                return NotFound(users.Error);

            return Ok(users.Value);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<AppUserDto>> GetUser(string username)
        {
            var user = await _userService.GetAppUserByUsername(username);

            if (!user.Success)
                return NotFound(user.Error);

            return Ok(user.Value);
        }
    }
}
