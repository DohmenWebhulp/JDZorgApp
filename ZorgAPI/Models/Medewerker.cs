using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZorgApp2.Models
{
    public class Medewerker
    {
        [Key]
        public int Id { get; set; }
        public Guid GUID { get; set; }
        public string Gebruikersnaam { get; set; }
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }
        public List<Bezoek> Bezoeken { get; set; }

    }
}
