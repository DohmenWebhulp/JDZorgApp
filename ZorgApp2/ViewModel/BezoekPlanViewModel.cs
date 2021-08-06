using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Models;

namespace ZorgApp2.ViewModel
{
    public class BezoekPlanViewModel
    {
        public List<Klant> Klanten { get; set; }
        public List<Medewerker> Medewerkers { get; set; }
        public List<Taak> Taken { get; set; }
        public Bezoek Bezoek { get; set; }
    }
}
