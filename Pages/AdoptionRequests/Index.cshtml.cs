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

namespace PetMatch.Pages.AdoptionRequests
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly PetMatch.Data.PetMatchContext _context;

        public IndexModel(PetMatch.Data.PetMatchContext context)
        {
            _context = context;
        }

        public IList<AdoptionRequest> AdoptionRequest { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.AdoptionRequest != null)
            {
                AdoptionRequest = await _context.AdoptionRequest
                    .Include(a => a.Animal) 
                    .Include(a => a.Member) 
                    .ToListAsync();
            }
        }

            public async Task<IActionResult> OnPostApproveAsync(int id)
        {
            var request = await _context.AdoptionRequest.FindAsync(id);
            if (request != null)
            {
                request.Status = "Approved"; 
                await _context.SaveChangesAsync(); 
            }
            return RedirectToPage(); 
        }

        public async Task<IActionResult> OnPostRejectAsync(int id)
        {
            var request = await _context.AdoptionRequest.FindAsync(id);
            if (request != null)
            {
                request.Status = "Rejected";
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
