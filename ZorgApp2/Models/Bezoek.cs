using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZorgApp2.Models
{
    public class Bezoek
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan Tijdstip_aanmelden { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan Tijdstip_afmelden { get; set; }
        public int MedewerkerId { get; set; }
        public int PlannerId { get; set; }
        public int KlantId { get; set; }
        public Medewerker Medewerker { get; set; }
        public Planner Planner { get; set; }
        public Klant Klant { get; set; }
        public List<Handeling> Handelingen { get; set; }



    }
}
