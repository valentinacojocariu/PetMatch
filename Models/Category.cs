using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PetMatch.Models
{
    public class Category
    {
        public int ID { get; set; }

        // AM SCHIMBAT DIN 'CategoryName' ÎN 'Name'
        // Asta trebuie să fie aici ca să se potrivească cu ce ai modificat în Controller și Views
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Animal>? Animals { get; set; }
    }
}