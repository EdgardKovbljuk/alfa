using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace alpha
{
    /// <summary>
    /// Statická třída RozvrhHodnotitel slouží k hodnocení seznamu rozvrhů pomocí zadaného hodnotitele.
    /// </summary>
    public static class RozvrhHodnotitel
    {
        /// <summary>
        /// Asynchronně hodnotí seznam rozvrhů pomocí zadaného hodnotitele.
        /// </summary>
        /// <param name="hodnotitel">Hodnotitel, který bude hodnotit rozvrhy.</param>
        /// <param name="rozvrhy">Seznam rozvrhů k hodnocení.</param>
        /// <returns>Seznam hodnocení rozvrhů, kde každý rozvrh má přiřazené skóre.</returns>
        /// <exception cref="ArgumentNullException">Vyhazuje výjimku, pokud hodnotitel nebo seznam rozvrhů jsou null.</exception>
        public static async Task<List<RozvrhHodnoceni>> HodnotitRozvrhyAsync(Hodnotitel hodnotitel, List<Rozvrh> rozvrhy)
        {
            if (hodnotitel == null)
            {
                throw new ArgumentNullException(nameof(hodnotitel), "Hodnotitel nesmí být null.");
            }

            if (rozvrhy == null)
            {
                throw new ArgumentNullException(nameof(rozvrhy), "Seznam rozvrhů nesmí být null.");
            }

            var hodnoceni = new List<RozvrhHodnoceni>();

            foreach (var rozvrh in rozvrhy)
            {
                try
                {
                    int skore = await hodnotitel.OhodnotRozvrhAsync(rozvrh);
                    hodnoceni.Add(new RozvrhHodnoceni { Rozvrh = rozvrh, Skore = skore });
                    Interlocked.Increment(ref Program.pocetOhodnocenychRozvrhu);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Nastala chyba při hodnocení rozvrhu: {e.Message}");
                    
                }
            }

            return hodnoceni;
        }
    }
}
