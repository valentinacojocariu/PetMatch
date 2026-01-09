using System.ComponentModel.DataAnnotations;

namespace PetMatch.Models
{
    public class Shelter
    {
        public int ID { get; set; }

        [Display(Name = "Nume Adăpost")]
        [Required(ErrorMessage = "Numele adăpostului este obligatoriu")]
        public string Name { get; set; }

        [Display(Name = "Locație")]
        public string? Location { get; set; }

        public ICollection<Animal>? Animals { get; set; }
    }
}
