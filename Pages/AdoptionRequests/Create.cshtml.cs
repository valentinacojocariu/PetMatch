using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetMatch.Data;
using PetMatch.Models;

namespace PetMatch.Pages.AdoptionRequests
{
    public class CreateModel : PageModel
    {
        private readonly PetMatch.Data.PetMatchContext _context;

        public CreateModel(PetMatch.Data.PetMatchContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? animalId)
        {
            ViewData["AnimalID"] = new SelectList(_context.Animal, "ID", "Name", animalId);

            var loggedInUser = User.Identity?.Name;

            var currentMember = _context.Member
                .FirstOrDefault(m => m.FullName == loggedInUser || m.Email == loggedInUser);

            if (currentMember != null)
            {
                ViewData["MemberID"] = new SelectList(new List<Member> { currentMember }, "ID", "FullName");
            }
            else
            {
                ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
            }

            return Page();
        }

        [BindProperty]
        public AdoptionRequest AdoptionRequest { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            AdoptionRequest.Status = "Pending";

            ModelState.Remove("AdoptionRequest.Status");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AdoptionRequest.Add(AdoptionRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}