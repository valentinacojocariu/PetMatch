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

        // 1. Trimite o cerere de adopție
        [HttpPost("request")]
        public async Task<IActionResult> RequestAdoption([FromBody] AdoptionRequestDto request)
        {
            // Aici ai crea o intrare intr-un tabel 'AdoptionRequests' in baza de date
            // Pentru simplificare acum, doar simulam succesul
            // Daca ai un tabel real, aici faci _context.Requests.Add(...)

            return Ok(new { message = "Cererea a fost trimisă către admin!" });
        }

        // 2. Verifică statusul cererilor mele (Notificări)
        [HttpGet("myrequests")]
        public async Task<IActionResult> GetMyRequests(string email)
        {
            // Aici returnezi lista cererilor pentru userul respectiv
            // Exemplu simulat pentru demo:
            var mockRequests = new List<object>
            {
                new { AnimalName = "Ricky", Status = "Aprobat", Message = "Vino să îl iei mâine!" }
            };
            return Ok(mockRequests);
        }
    }

    public class AdoptionRequestDto { public int AnimalId { get; set; } public string UserEmail { get; set; } }
}