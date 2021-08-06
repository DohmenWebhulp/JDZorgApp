using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Models;

namespace ZorgApp2.ViewModel
{
    public class KlantViewModel
    {
        public Klant Klant { get; set; }
        public List<Taak> Taken { get; set; }
    }
}
