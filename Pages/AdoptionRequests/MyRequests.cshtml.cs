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
            // 1. Aflăm cine este logat (Emailul sau User-ul)
            // Varianta simplă: presupunem că numele membrului sau emailul corespunde cu User.Identity.Name
            // Sau, dacă ai legat membrul de user, filtrăm după asta.

            var currentUserEmail = User.Identity?.Name;

            if (_context.AdoptionRequest != null)
            {
                // 2. Aducem cererile DOAR pentru acest user
                // ATENȚIE: Aici presupunem că în tabela Members ai o coloană Email care corespunde cu login-ul
                // Dacă nu ai, va trebui să modifici condiția .Where()

                MyAdoptionRequests = await _context.AdoptionRequest
                    .Include(a => a.Animal)  // Să vedem numele câinelui
                    .Include(a => a.Member)  // Să vedem datele membrului
                                             // Filtrăm: Luăm cererile unde Email-ul membrului este egal cu cel logat
                                             // SAU (dacă nu ai email la membru) poți scoate .Where momentan să vezi dacă merge lista
                    .Where(r => r.Member.Email == currentUserEmail)
                    .ToListAsync();
            }
        }
    }
}