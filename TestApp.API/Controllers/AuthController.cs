using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApp.API.Data;
using TestApp.API.Dtos;
using TestApp.API.Models;

namespace TestApp.API.Controllers
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
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto) 
        // You can use [FromBody] if you dont use [ApiController] 
        {
            userRegisterDto.Username = userRegisterDto.Username.ToLower();
            // if(!ModelState.IsValid){
            //     return BadRequest(ModelState);
            // }
            if (await _repo.UserExist(userRegisterDto.Username))
            {
                return BadRequest("Username already exist");
            }

            var userToCreate = new User
            {
                Username = userRegisterDto.Username
            };
            
            var createdUser = await _repo.Register(userToCreate,userRegisterDto.Password);

            return Ok();
        }
    }
}