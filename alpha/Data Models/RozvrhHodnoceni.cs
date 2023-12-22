using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alpha
{
    /// <summary>
    /// Třída pro uchování hodnocení konkrétního rozvrhu.
    /// </summary>
    public class RozvrhHodnoceni
    {
        /// <summary>
        /// Instance rozvrhu, která je hodnocena.
        /// </summary>
        public Rozvrh Rozvrh { get; set; }

        /// <summary>
        /// Numerické skóre přidělené rozvrhu. Vyšší hodnota značí lepší rozvrh.
        /// </summary>
        public int Skore { get; set; }
    }
}
