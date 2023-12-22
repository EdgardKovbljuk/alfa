using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alpha
{
    /// <summary>
    /// Statická třída poskytující pomocné metody pro práci s rozvrhy.
    /// </summary>
    public static class RozvrhUtils
    {
        /// <summary>
        /// Rozdělí seznam rozvrhů na menší skupiny.
        /// </summary>
        /// <param name="rozvrhy">Seznam rozvrhů k rozdělení.</param>
        /// <param name="pocetSkupin">Počet skupin, na které se mají rozvrhy rozdělit.</param>
        /// <returns>Seznam seznamů rozvrhů rozdělených do skupin.</returns>
        public static List<List<Rozvrh>> RozdelitRozvrhy(List<Rozvrh> rozvrhy, int pocetSkupin)
        {
            var rozdeleneRozvrhy = new List<List<Rozvrh>>();
            int velikostSkupiny = rozvrhy.Count / pocetSkupin;

            for (int i = 0; i < pocetSkupin; i++)
            {
                rozdeleneRozvrhy.Add(rozvrhy.Skip(i * velikostSkupiny).Take(velikostSkupiny).ToList());
            }

            return rozdeleneRozvrhy;
        }


        /// <summary>
        /// Uloží seznam hodnocení rozvrhů do souboru.
        /// </summary>
        /// <param name="rozvrhy">Seznam hodnocení rozvrhů k uložení.</param>
        /// <param name="cesta">Cesta k souboru, do kterého se mají rozvrhy uložit.</param>
        /// <remarks>
        /// Tato metoda může vyvolat výjimku <see cref="System.IO.IOException"/> v případě,
        /// že dojde k chybě při zápisu do souboru.
        /// </remarks>
        public static void UlozitRozvrhyDoSouboru(IEnumerable<RozvrhHodnoceni> rozvrhy, string cesta)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(cesta))
                {
                    foreach (var rozvrhHodnoceni in rozvrhy)
                    {
                        sw.WriteLine($"Skóre rozvrhu: {rozvrhHodnoceni.Skore}");
                        VypisDetailyRozvrhu(sw, rozvrhHodnoceni.Rozvrh);
                        sw.WriteLine("-----------------------------------");
                    }
                }
            }
            catch (IOException e)
            {
                throw new Exception("Došlo k chybě při zápisu do souboru: " + e.Message);
            }
        }




        /// <summary>
        /// Vybere nejlepší rozvrhy na základě hodnocení zadaných hodnotitelů.
        /// </summary>
        /// <param name="hodnotiteleResults">Seznam úloh hodnotitelů s jejich výsledky.</param>
        /// <param name="pocetNejlepsich">Počet nejlepších rozvrhů, které se mají vybrat.</param>
        /// <returns>Seznam nejlepších rozvrhů.</returns>
        public static IEnumerable<RozvrhHodnoceni> VybratNejlepsiRozvrhy(List<Task<List<RozvrhHodnoceni>>> hodnotiteleResults, int pocetNejlepsich)
        {
            return hodnotiteleResults.SelectMany(task => task.Result)
                                     .OrderByDescending(r => r.Skore)
                                     .Take(pocetNejlepsich);
        }


        /// <summary>
        /// Vypíše detaily daného rozvrhu do stream writeru.
        /// </summary>
        /// <param name="sw">Stream writer, do kterého se mají detaily rozvrhu vypsat.</param>
        /// <param name="rozvrh">Rozvrh, jehož detaily se mají vypsat.</param>
        private static void VypisDetailyRozvrhu(StreamWriter sw, Rozvrh rozvrh)
        {
            for (int den = 0; den < rozvrh.Hodiny.GetLength(0); den++) // Pro každý den
            {
                sw.WriteLine($"Den {den + 1}:");

                for (int hodina = 0; hodina < rozvrh.Hodiny.GetLength(1); hodina++) // Pro každou hodinu
                {
                    var hodinaInfo = rozvrh.Hodiny[den, hodina];

                    if (hodinaInfo != null)
                    {
                        string predmet = hodinaInfo.Predmet?.Nazev ?? "Volno";
                        string typ = hodinaInfo.Predmet?.Typ == TypPredmetu.Teorie ? "teorie" : "cvičení";
                        string ucitel = hodinaInfo.Predmet?.Ucitel?.Jmeno ?? "Neznámý";
                        string ucebna = hodinaInfo.Predmet?.Ucebna?.Cislo ?? "Neznámá";

                        sw.WriteLine($"  Hodina {hodina + 1}: {predmet} ({typ}), Učitel: {ucitel}, Učebna: {ucebna}");
                    }
                    else
                    {
                        sw.WriteLine($"  Hodina {hodina + 1}: Volno");
                    }
                }

                sw.WriteLine();
            }
        }
    }
}
