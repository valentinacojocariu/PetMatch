using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetMatch.Models;

namespace PetMatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Verificăm dacă user-ul există
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Email sau parola gresita!" });
            }

            // Verificăm parola
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                // Returnăm un mesaj de succes (și eventual datele userului)
                return Ok(new { email = user.Email, id = user.Id });
            }

            return Unauthorized(new { message = "Email sau parola gresita!" });
        }
    }
}