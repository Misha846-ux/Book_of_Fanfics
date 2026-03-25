using System.Security.Claims;
using System.Threading.Tasks;
using FanfictionBook.Application.DTOs.UserDTOs;
using FanfictionBook.Application.Interfaces.Services;
using FanfictionBook.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FanfictionBook.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UserController(IUserService _service, IJwtService _jwtService): ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            ICollection<UserGetDto> users = await _service.GetAllUsersAsync(cancellationToken);
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserPostDto user, CancellationToken cancellationToken)
        {
            int id = await _service.CreateUserAsync(user, cancellationToken);
            return Ok(id);
        }
        [HttpPost("Login/password")]
        public async Task<IActionResult> LoginWithPassword(UserLoginDto loginDto, CancellationToken cancellationToken)
        {
            string token = await _service.LoginWithPasswordAsync(loginDto, cancellationToken);

            return Ok(new { token });
        }

        /// <summary>
        /// Смысл в том что как зареганый пользователь ты можешь поменять пороль на акаунте через ChangeUser
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("ForgotPassword/GetToken{email}")]
        public async Task<IActionResult> ForgotPassword([FromRoute] string email, CancellationToken cancellationToken)
        {
            string token = await _service.CreateRecoveryTokenAsync(email, cancellationToken);
            return Ok(token);
        }
        [HttpPost("ForgotPassword/LoginWithToken")]
        public async Task<IActionResult> ForgotPasswordLogin([FromBody] UserLoginDto userLogin, CancellationToken cancellationToken)
        {
            string token = await _service.LoginWithRecoveryTokenAsync(userLogin, cancellationToken);

            return Ok(new {token});
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangeUser(UserUpdateDto newUser, CancellationToken cancellationToken)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            UserGetDto user = await _service.GetUserByEmailAsync(email, cancellationToken);

            newUser.Id = user.Id;
            UserGetDto output = await _service.UpdateUserAsync(newUser, cancellationToken);
            return Ok(output);

        }
    }
}
