using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetMatch.Data;
using PetMatch.Models;
using Microsoft.AspNetCore.Hosting; // Necesar pentru fișiere

namespace PetMatch.Pages.Animals
{
    public class EditModel : PageModel
    {
        private readonly PetMatchContext _context;
        private readonly IWebHostEnvironment _environment; // Unealta pentru foldere

        public EditModel(PetMatchContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Animal Animal { get; set; } = default!;

        // Variabila temporară pentru poza nouă
        [BindProperty]
        public IFormFile? AnimalImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Animal == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FirstOrDefaultAsync(m => m.ID == id);
            if (animal == null)
            {
                return NotFound();
            }
            Animal = animal;

            // Încărcăm listele pentru dropdown-uri
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "CategoryName");
            ViewData["ShelterID"] = new SelectList(_context.Shelter, "ID", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validare simplificată (opțional)
            /* if (!ModelState.IsValid) 
            {
                ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "CategoryName");
                ViewData["ShelterID"] = new SelectList(_context.Shelter, "ID", "Name");
                return Page();
            } */

            // --- LOGICA PENTRU SALVARE POZĂ ---
            if (AnimalImage != null)
            {
                // 1. Stabilim calea: wwwroot/images/animals
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "animals");

                // Creăm folderul dacă nu există
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // 2. Folosim numele exact al fișierului (ex: ricky.jpg)
                var uniqueFileName = AnimalImage.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // 3. Copiem poza în folder
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await AnimalImage.CopyToAsync(fileStream);
                }

                // 4. Actualizăm numele în baza de date
                Animal.ImageUrl = uniqueFileName;
            }
            else
            {
                // Dacă nu s-a încărcat o poză nouă, trebuie să ne asigurăm că nu ștergem link-ul vechi.
                // EF Core se ocupă de asta de obicei, dar e bine să fim siguri.
                var existingAnimal = await _context.Animal.AsNoTracking().FirstOrDefaultAsync(a => a.ID == Animal.ID);
                if (existingAnimal != null && string.IsNullOrEmpty(Animal.ImageUrl))
                {
                    Animal.ImageUrl = existingAnimal.ImageUrl;
                }
            }
            // ----------------------------------

            _context.Attach(Animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(Animal.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AnimalExists(int id)
        {
            return (_context.Animal?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}