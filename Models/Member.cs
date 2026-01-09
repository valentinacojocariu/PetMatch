using System.ComponentModel.DataAnnotations;

namespace PetMatch.Models
{
    public class Member
    {
        public int ID { get; set; }

        [Display(Name = "Nume Complet")]
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        public ICollection<AdoptionRequest>? AdoptionRequests { get; set; }
    }
}
