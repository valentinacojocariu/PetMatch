using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetMatch.Data;
using PetMatch.Models;

namespace PetMatch.Pages.Animals
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly PetMatch.Data.PetMatchContext _context;

        public DetailsModel(PetMatch.Data.PetMatchContext context)
        {
            _context = context;
        }

        public Animal Animal { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FirstOrDefaultAsync(m => m.ID == id);
            if (animal == null)
            {
                return NotFound();
            }
            else
            {
                Animal = animal;
            }
            return Page();
        }
    }
}
