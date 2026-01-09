using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetMatch.Models
{
    public class Animal
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Nume Animal")]
        public string Name { get; set; }

        public string? Breed { get; set; } 

        [Column(TypeName = "decimal(4, 1)")]
        [Display(Name = "Vârstă (Ani)")]
        public decimal Age { get; set; }

        [Display(Name = "Descriere")]
        public string? Description { get; set; }

        [Display(Name = "Poză (URL)")]
        public string? ImageUrl { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data Salvării")]
        public DateTime RescueDate { get; set; }

        [Display(Name = "Adăpost")]
        public int? ShelterID { get; set; }
        public Shelter? Shelter { get; set; }

        [Display(Name = "Categorie")]
        public int? CategoryID { get; set; }
        public Category? Category { get; set; }
        public bool IsAdopted { get; set; } = false; 
        public ICollection<AdoptionRequest>? AdoptionRequests { get; set; }
    }
}
