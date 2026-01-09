using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetMatch.Data;
using PetMatch.Models;

namespace PetMatch.Pages.AdoptionRequests
{
    public class DeleteModel : PageModel
    {
        private readonly PetMatch.Data.PetMatchContext _context;

        public DeleteModel(PetMatch.Data.PetMatchContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AdoptionRequest AdoptionRequest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionrequest = await _context.AdoptionRequest.FirstOrDefaultAsync(m => m.ID == id);

            if (adoptionrequest == null)
            {
                return NotFound();
            }
            else
            {
                AdoptionRequest = adoptionrequest;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionrequest = await _context.AdoptionRequest.FindAsync(id);
            if (adoptionrequest != null)
            {
                AdoptionRequest = adoptionrequest;
                _context.AdoptionRequest.Remove(AdoptionRequest);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
