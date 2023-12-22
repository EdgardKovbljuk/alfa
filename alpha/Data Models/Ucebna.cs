using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alpha
{
    /// <summary>
    /// Třída reprezentující učebnu.
    /// </summary>
    public class Ucebna
    {
        /// <summary>
        /// Jedinečné číslo nebo označení učebny.
        /// </summary>
        public string Cislo { get; set; }

        /// <summary>
        /// Inicializuje novou instanci třídy <see cref="Ucebna"/> s daným číslem.
        /// </summary>
        /// <param name="cislo">Číslo nebo označení učebny.</param>
        public Ucebna(string cislo)
        {
            Cislo = cislo;
        }
    }
}
