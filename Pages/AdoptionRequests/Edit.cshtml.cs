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

namespace PetMatch.Pages.AdoptionRequests
{
    public class EditModel : PageModel
    {
        private readonly PetMatch.Data.PetMatchContext _context;

        public EditModel(PetMatch.Data.PetMatchContext context)
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

            var adoptionrequest =  await _context.AdoptionRequest.FirstOrDefaultAsync(m => m.ID == id);
            if (adoptionrequest == null)
            {
                return NotFound();
            }
            AdoptionRequest = adoptionrequest;
           ViewData["AnimalID"] = new SelectList(_context.Animal, "ID", "Name");
           ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "ID", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AdoptionRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdoptionRequestExists(AdoptionRequest.ID))
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

        private bool AdoptionRequestExists(int id)
        {
            return _context.AdoptionRequest.Any(e => e.ID == id);
        }
    }
}
