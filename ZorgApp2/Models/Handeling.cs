using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZorgApp2.Models
{
    public class Handeling
    {
        public int Id { get; set; }
        public bool Uitvoering { get; set; }
        public int TaakId { get; set; }
        public int BezoekId { get; set; }
        public Taak Taak { get; set; }
        public Bezoek Bezoek { get; set; }
    }
}
