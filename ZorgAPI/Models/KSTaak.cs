using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZorgApp2.Models
{
    public class KSTaak
    {
        public int Id { get; set; }
        public int TaakId { get; set; }
        public int KlantId { get; set; }
        public Klant Klant { get; set; }
        public Taak Taak { get; set; }
    }
}
