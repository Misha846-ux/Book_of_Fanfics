using System.Threading.Tasks;
using FanfictionBook.Application.DTOs.UserDTOs;
using FanfictionBook.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FanfictionBook.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UserController(IUserService _service): ControllerBase
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
    }
}
