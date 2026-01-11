using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetMatch.Data;
using PetMatch.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly PetMatchContext _context;

        public AnimalsController(PetMatchContext context)
        {
            _context = context;
        }

        // GET: api/Animals
        // Returnează lista tuturor animalelor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
            // Include și Shelter/Category ca să avem toate datele pe mobil
            return await _context.Animal
                .Include(a => a.Shelter)
                .Include(a => a.Category)
                .ToListAsync();
        }

        // GET: api/Animals/5
        // Returnează un singur animal după ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await _context.Animal
                         .Include(a => a.Shelter)
                         .Include(a => a.Category)
                         .FirstOrDefaultAsync(a => a.ID == id);

            if (animal == null)
            {
                return NotFound();
            }

            return animal;
        }

        // POST: api/Animals
        // Adaugă un animal nou (pentru când vei face Create de pe mobil)
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
            _context.Animal.Add(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnimal", new { id = animal.ID }, animal);
        }

        // DELETE: api/Animals/5
        // Șterge un animal
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var animal = await _context.Animal.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animal.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Animals/5
        // Modifică un animal
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(int id, Animal animal)
        {
            if (id != animal.ID)
            {
                return BadRequest();
            }

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool AnimalExists(int id)
        {
            return _context.Animal.Any(e => e.ID == id);
        }
    }
}

