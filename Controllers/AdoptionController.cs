using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetMatch.Data;
using PetMatch.Models;

namespace PetMatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoptionController : ControllerBase
    {
        private readonly PetMatchContext _context;

        public AdoptionController(PetMatchContext context)
        {
            _context = context;
        }

        [HttpPost("request")]
        public async Task<IActionResult> CreateRequest([FromBody] dynamic data)
        {
            try
            {
                int animalId = data.GetProperty("animalId").GetInt32();
                string email = data.GetProperty("userEmail").GetString();

                var member = await _context.Member.FirstOrDefaultAsync(m => m.Email == email);
                if (member == null) return BadRequest("Utilizator negăsit.");

                var request = new AdoptionRequest
                {
                    AnimalID = animalId,
                    MemberID = member.ID,
                    RequestDate = DateTime.Now,
                    Status = "În așteptare"
                };

                _context.AdoptionRequest.Add(request);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Cerere salvată!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("myrequests")]
        public async Task<IActionResult> GetMyRequests([FromQuery] string email)
        {
            var requests = await _context.AdoptionRequest
                .Include(r => r.Animal)
                .Where(r => r.Member.Email == email)
                .Select(r => new {
                    Id = r.ID,
                    AnimalName = r.Animal.Name,
                    Status = r.Status 
                })
                .ToListAsync();

            return Ok(requests);
        }
    }
}