using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZorgApp2.Models
{
    public class Klant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Naam { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }
        public string Woonplaats { get; set; }
        public List<Bezoek> Bezoeken { get; set; }
        public List<KSTaak> KSTaken { get; set; }
    }
}
