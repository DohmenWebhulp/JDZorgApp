using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZorgApp2.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string KlantNaam { get; set; }
        public string KlantAdres { get; set; }
        public string KlantPostcode { get; set; }
        public string KlantWoonplaats { get; set; }
        public string StartDatum { get; set; }
    }
}
