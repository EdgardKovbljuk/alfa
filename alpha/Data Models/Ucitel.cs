using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alpha
{
    /// <summary>
    /// Třída reprezentující učitele.
    /// </summary>
    public class Ucitel
    {
        /// <summary>
        /// Získá nebo nastaví jméno učitele.
        /// </summary>
        public string Jmeno { get; set; }

        /// <summary>
        /// Inicializuje novou instanci třídy <see cref="Ucitel"/> s určeným jménem.
        /// </summary>
        /// <param name="jmeno">Jméno učitele.</param>
        public Ucitel(string jmeno)
        {
            Jmeno = jmeno;
        }
    }
}
