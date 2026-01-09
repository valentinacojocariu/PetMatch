using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PetMatch.Models;

namespace PetMatch.Data
{
    public class PetMatchContext : IdentityDbContext
    {
        public PetMatchContext (DbContextOptions<PetMatchContext> options)
            : base(options)
        {
        }

        public DbSet<PetMatch.Models.Animal> Animal { get; set; } = default!;
        public DbSet<PetMatch.Models.Category> Category { get; set; } = default!;
        public DbSet<PetMatch.Models.AdoptionRequest> AdoptionRequest { get; set; } = default!;
        public DbSet<PetMatch.Models.Member> Member { get; set; } = default!;
        public DbSet<PetMatch.Models.Shelter> Shelter { get; set; } = default!;
    }
}
