using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Repositories;
using ZorgApp2.Models;
using ZorgApp2.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ZorgApp2.Repositories
{
    public class BezoekRepository : IBezoekRepository
    {
        private readonly ZorgAppDbContext _context;
        public BezoekRepository(ZorgAppDbContext context)
        {
            _context = context;
        }

        public Bezoek OphalenBezoek(int? id)
        {
            Bezoek Bezoek = _context.Bezoek.Include(c => c.Handelingen).FirstOrDefault(u => u.Id == id);
            return Bezoek;
        }

        //Lijst Met Bezoeken. Voor Bezoeken overzicht.
        public List<Bezoek> OphalenBezoeken()
        {
            List<Bezoek> Bezoeken = _context.Bezoek.Include(c => c.Klant).Include(d => d.Medewerker)
                                                   .ToList();
            return Bezoeken;
        }

        public Bezoek UpdateBezoek(Bezoek Bezoek)
        {
            //Op de bezoekplanpagina moeten een klant en een medewerker worden geselecteerd.
            //Wanneer ze geselecteerd worden, moet vervolgens de informatie betreffende die klant/medewerker worden getoond op de bezoekplanpagina.
            //Deze informatie kan echter alleen worden getoond als de info over het betreffende bezoek opgeslagen wordt in de database,
            //aangezien het bezoek na een redirect opnieuw wordt opgehaald om de pagina te herladen.
            //Nu wordt dit bezoek met alle relevante informatie (FK's inbegrepen) doorgestuurd naar de updatebezoek functie.
            //Maar dan volgt er een error aangezien er twee dingen door elkaar gaan:

            //System.InvalidOperationException: 'The instance of entity type 'Bezoek' cannot be tracked because
            //another instance with the same key value
            //for {'Id'} is already being tracked. When attaching existing entities, ensure that only one entity
            //instance with a given key value is attached.
            //Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.'

            //Hoe dit het beste op te lossen? Een vraag die ook vanuit Frank kwam is:
            //Kun je het doorgegeven bezoek niet klonen en de kloon gebruiken om de database te updaten?
            Bezoek bez = _context.Bezoek.Find(Bezoek.Id);
            bez.KlantId = Bezoek.KlantId;
            bez.MedewerkerId = Bezoek.MedewerkerId;
            bez.Datum = Bezoek.Datum;
            _context.SaveChanges();
            return bez;
        }

        public Bezoek ToevoegenBezoek(Bezoek Bezoek)
        {
            _context.Bezoek.Add(Bezoek);
            _context.SaveChanges();
            return Bezoek;
        }

        public Bezoek VerwijderBezoek(int BezoekId)
        {
            Bezoek Bezoek = _context.Bezoek.Include(c => c.Handelingen).FirstOrDefault(u => u.Id == BezoekId);
            if (Bezoek != null)
            {
                foreach (Handeling handeling in Bezoek.Handelingen)
                {
                    _context.Handeling.Remove(handeling);
                }
                _context.Bezoek.Remove(Bezoek);
                _context.SaveChanges();
            }
            return Bezoek;
        }
    }
}
