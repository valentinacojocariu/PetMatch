using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetMatch.Data;
using PetMatch.Models;

namespace PetMatch.Pages.Shelters
{
    public class DeleteModel : PageModel
    {
        private readonly PetMatch.Data.PetMatchContext _context;

        public DeleteModel(PetMatch.Data.PetMatchContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Shelter Shelter { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelter = await _context.Shelter.FirstOrDefaultAsync(m => m.ID == id);

            if (shelter == null)
            {
                return NotFound();
            }
            else
            {
                Shelter = shelter;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelter = await _context.Shelter.FindAsync(id);
            if (shelter != null)
            {
                Shelter = shelter;
                _context.Shelter.Remove(Shelter);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
