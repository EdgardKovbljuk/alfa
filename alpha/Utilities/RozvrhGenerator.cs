using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace alpha
{
    /// <summary>
    /// Třída RozvrhGenerator slouží k generování náhodných rozvrhů na základě zadaného seznamu předmětů.
    /// </summary>
    public class RozvrhGenerator
    {
        private readonly List<Predmet> predmety;
        private readonly Random random;

        /// <summary>
        /// Počet vygenerovaných rozvrhů.
        /// </summary>
        public int PocetVygenerovanychRozvrhu { get; private set; } = 0;

        /// <summary>
        /// Inicializuje novou instanci třídy RozvrhGenerator s daným seznamem předmětů.
        /// </summary>
        /// <param name="predmety">Seznam předmětů použitých pro generování rozvrhů.</param>
        /// <exception cref="ArgumentNullException">Vyhazuje, když je seznam předmětů null.</exception>
        public RozvrhGenerator(List<Predmet> predmety)
        {
            this.predmety = predmety ?? throw new ArgumentNullException(nameof(predmety), "Seznam předmětů nesmí být null.");
            random = new Random();
        }

        /// <summary>
        /// Asynchronně generuje rozvrhy dokud není vyžádáno zastavení procesu.
        /// </summary>
        /// <param name="cancellationToken">Token pro zastavení generování rozvrhů.</param>
        /// <returns>Kolekci vygenerovaných rozvrhů.</returns>
        public async Task<IEnumerable<Rozvrh>> GenerovatRozvrhyAsync(CancellationToken cancellationToken)
        {
            var rozvrhy = new List<Rozvrh>();

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var lokalniPredmety = ResetovatPredmety();
                    var rozvrh = new Rozvrh(5, 10); // Předpokládáme 5 dní a 10 hodin denně

                    for (int den = 0; den < 5; den++)
                    {
                        for (int hodina = 0; hodina < 10; hodina++)
                        {
                            Predmet vybranyPredmet = VybratNahodnyPredmet(lokalniPredmety);
                            if (vybranyPredmet != null)
                            {
                                rozvrh.NastavitHodinu(den, hodina, new Hodina(vybranyPredmet, null, null));
                                vybranyPredmet.PocetVyskytu--;
                            }
                        }
                    }

                    rozvrhy.Add(rozvrh);
                    PocetVygenerovanychRozvrhu++;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Došlo k chybě při generování rozvrhu: {e.Message}");
                    
                }
            }

            return rozvrhy;
        }

        private List<Predmet> ResetovatPredmety()
        {
            return new List<Predmet>(this.predmety.Select(p => new Predmet(p.Nazev, p.Typ, p.Ucitel, p.Ucebna, p.PocetVyskytu)));
        }

        private Predmet VybratNahodnyPredmet(List<Predmet> predmety)
        {
            var dostupnePredmety = predmety.Where(p => p.PocetVyskytu > 0).ToList();

            if (dostupnePredmety.Count == 0)
            {
                return null; // Nejsou dostupné žádné předměty
            }

            int index = random.Next(dostupnePredmety.Count);
            return dostupnePredmety[index];
        }

        public void VypsatRozvrhy(IEnumerable<Rozvrh> rozvrhy)
        {
            foreach (var rozvrh in rozvrhy)
            {
                Console.WriteLine("Vygenerován nový rozvrh:");
                for (int den = 0; den < rozvrh.Hodiny.GetLength(0); den++)
                {
                    Console.WriteLine($"Den {den + 1}:");
                    for (int hodina = 0; hodina < rozvrh.Hodiny.GetLength(1); hodina++)
                    {
                        var hodinaInfo = rozvrh.Hodiny[den, hodina];
                        if (hodinaInfo != null && hodinaInfo.Predmet != null)
                        {
                            string predmet = hodinaInfo.Predmet.Nazev;
                            string typ = hodinaInfo.Predmet?.Typ == TypPredmetu.Teorie ? "teorie" : "cvičení";
                            string ucitel = hodinaInfo.Predmet.Ucitel?.Jmeno ?? "Neznámý";
                            string ucebna = hodinaInfo.Predmet.Ucebna?.Cislo ?? "Neznámá";
                            Console.WriteLine($"  Hodina {hodina + 1}: {predmet} ({typ}), Učitel: {ucitel}, Učebna: {ucebna}");
                        }
                        else
                        {
                            Console.WriteLine($"  Hodina {hodina + 1}: Volno");
                        }
                    }
                    Console.WriteLine("-------------------------------");
                }
            }
        }
    }
}
