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

namespace PetMatch.Pages.Shelters
{
    
    public class IndexModel : PageModel
    {
        private readonly PetMatch.Data.PetMatchContext _context;

        public IndexModel(PetMatch.Data.PetMatchContext context)
        {
            _context = context;
        }

        public IList<Shelter> Shelter { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Shelter = await _context.Shelter.ToListAsync();
        }
    }
}
