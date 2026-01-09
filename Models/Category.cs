using System.ComponentModel.DataAnnotations;

namespace PetMatch.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Display(Name = "Categorie")]
        public string CategoryName { get; set; } 

        public ICollection<Animal>? Animals { get; set; }
    }
}
