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
    public class DetailsModel : PageModel
    {
        private readonly PetMatch.Data.PetMatchContext _context;

        public DetailsModel(PetMatch.Data.PetMatchContext context)
        {
            _context = context;
        }

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
    }
}
