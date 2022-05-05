using System.Security.Claims;
using System.Threading.Tasks;
using server_app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace server_app.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly  SignInManager<ApplicationUser> _signinManager;
        //private readonly TokenService _tokenService;
        public AccountController(UserManager<ApplicationUser> userManager
            ,SignInManager<ApplicationUser> signinManager
             //,TokenService tokenService
             )
        {
            //_tokenService = tokenService;
            _signinManager = signinManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login(LoginModel loginModel)
        {
            
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if(user == null) return Unauthorized();

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginModel.Password, false);

            if(result.Succeeded)
            {
                return CreateUserObject(user);
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register(RegisterModel registerModel)
        {
            if(await _userManager.Users.AnyAsync(x => x.Email == registerModel.Email))
            {
                ModelState.AddModelError("email", "Email taken");
                return ValidationProblem();
            }
            if(await _userManager.Users.AnyAsync(x => x.UserName == registerModel.Username))
            {
                ModelState.AddModelError("username", "Username taken");
                return ValidationProblem();
            }

            var user = new ApplicationUser
            {
                Email = registerModel.Email,
                UserName = registerModel.Username
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if(result.Succeeded)
            {
                return CreateUserObject(user);
            }

            return BadRequest("Problem regist User");
        }
/*
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            return CreateUserObject(user);
        }
*/
        private UserModel CreateUserObject(ApplicationUser user)
        {
            return new UserModel
            {
//                    Token = _tokenService.CreateToken(user),
                    Username = user.UserName
            };
        }
    }
}