using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alpha
{
    /// <summary>
    /// Reprezentuje jednu vyučovací hodinu, včetně předmětu, učitele a učebny.
    /// </summary>
    public class Hodina
    {
        /// <summary>
        /// Získává nebo nastavuje předmět hodiny.
        /// </summary>
        /// <value>
        /// Předmět hodiny.
        /// </value>
        public Predmet Predmet { get; set; }

        /// <summary>
        /// Získává nebo nastavuje učitele hodiny.
        /// </summary>
        /// <value>
        /// Učitel hodiny.
        /// </value>
        public Ucitel Ucitel { get; set; }

        /// <summary>
        /// Získává nebo nastavuje učebnu, kde se hodina koná.
        /// </summary>
        /// <value>
        /// Učebna pro hodinu.
        /// </value>
        public Ucebna Ucebna { get; set; }

        /// <summary>
        /// Inicializuje novou instanci třídy <see cref="Hodina"/> s určitým předmětem, učitelem a učebnou.
        /// </summary>
        /// <param name="predmet">Předmět, který se bude v hodině vyučovat.</param>
        /// <param name="ucitel">Učitel, který bude hodinu vyučovat.</param>
        /// <param name="ucebna">Učebna, kde se hodina bude konat.</param>
        public Hodina(Predmet predmet, Ucitel ucitel, Ucebna ucebna)
        {
            Predmet = predmet;
            Ucitel = ucitel;
            Ucebna = ucebna;
        }
    }
}
