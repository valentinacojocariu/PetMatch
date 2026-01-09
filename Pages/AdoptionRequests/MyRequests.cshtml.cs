using global::PetMatch.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetMatch.Models;

namespace PetMatch.Pages.AdoptionRequests
{
    public class MyRequestsModel : PageModel
    {
        private readonly PetMatch.Data.PetMatchContext _context;

        public MyRequestsModel(PetMatch.Data.PetMatchContext context)
        {
            _context = context;
        }

        public IList<AdoptionRequest> MyAdoptionRequests { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var loggedInUser = User.Identity?.Name;

            if (string.IsNullOrEmpty(loggedInUser))
            {
                MyAdoptionRequests = new List<AdoptionRequest>();
                return;
            }

            var currentMember = await _context.Member
                .FirstOrDefaultAsync(m => m.Email == loggedInUser || m.FullName == loggedInUser);

            if (currentMember != null)
            {
                MyAdoptionRequests = await _context.AdoptionRequest
                    .Include(a => a.Animal)
                    .Include(a => a.Member)
                    .Where(r => r.MemberID == currentMember.ID)
                    .ToListAsync();
            }
            else
            {
                MyAdoptionRequests = new List<AdoptionRequest>();
            }
        }
    }
}