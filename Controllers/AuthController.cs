using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetMatch.Models; // Asigura-te ca ai namespace-ul corect

namespace PetMatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "Cont creat cu succes!" });
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                return Ok(new { message = "Login reușit!", email = model.Email });
            }
            return Unauthorized("Email sau parolă incorectă.");
        }
    }

    // Modele simple pentru datele primite de la telefon
    public class RegisterModel { public string Email { get; set; } public string Password { get; set; } }
    public class LoginModel { public string Email { get; set; } public string Password { get; set; } }
}