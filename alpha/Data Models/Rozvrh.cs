using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alpha
{
    /// <summary>
    /// Třída reprezentující rozvrh, který obsahuje hodiny pro určité dny a časy.
    /// </summary>
    public class Rozvrh
    {
        /// <summary>
        /// Dvourozměrné pole obsahující hodiny rozvrhu.
        /// </summary>
        public Hodina[,] Hodiny { get; private set; }

        /// <summary>
        /// Konstruktor pro vytvoření instance rozvrhu.
        /// </summary>
        /// <param name="pocetDnu">Počet dnů v rozvrhu.</param>
        /// <param name="pocetHodin">Počet hodin v každém dni rozvrhu.</param>
        public Rozvrh(int pocetDnu, int pocetHodin)
        {
            Hodiny = new Hodina[pocetDnu, pocetHodin];
        }

        /// <summary>
        /// Nastaví hodinu v rozvrhu na zadané pozici.
        /// </summary>
        /// <param name="den">Index dne v rozvrhu (např. 0 pro pondělí).</param>
        /// <param name="hodina">Index hodiny v daném dni (např. 0 pro první hodinu).</param>
        /// <param name="hodinaInfo">Instance třídy Hodina, která má být nastavena.</param>
        public void NastavitHodinu(int den, int hodina, Hodina hodinaInfo)
        {
            Hodiny[den, hodina] = hodinaInfo;
        }

 
    }
}
