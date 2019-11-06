using System.Threading.Tasks;
using DatingApplication.Data;
using DatingApplication.DTOs;
using DatingApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApplication.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDTO)
        {
            userForRegisterDTO.Username = userForRegisterDTO.Username.ToLower();

            if(await _repo.UserExists(userForRegisterDTO.Username))
            { return BadRequest("Username Already Exist"); }

            var userToCreate = new User{
                Username = userForRegisterDTO.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDTO.Password);

            return StatusCode(201);
        }
    }
}