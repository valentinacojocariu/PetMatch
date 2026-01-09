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
    public class DetailsModel : PageModel
    {
        private readonly PetMatch.Data.PetMatchContext _context;

        public DetailsModel(PetMatch.Data.PetMatchContext context)
        {
            _context = context;
        }

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
    }
}
