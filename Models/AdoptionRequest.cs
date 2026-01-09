using System.ComponentModel.DataAnnotations;

namespace PetMatch.Models
{
    public class AdoptionRequest
    {
        public int ID { get; set; }

        [Display(Name = "Data Cererii")]
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }

        [Display(Name = "Mesaj")]
        public string? Message { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; } = "Pending";
       
        [Display(Name = "Animal Dorit")]
        public int AnimalID { get; set; }
        public Animal? Animal { get; set; }

        [Display(Name = "Nume Membru")]
        public int MemberID { get; set; }
        public Member? Member { get; set; }
    }
}