using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alpha
{
    /// <summary>
    /// Třída reprezentující předmět v rozvrhu.
    /// </summary>
    public class Predmet
    {
        /// <summary>
        /// Název předmětu.
        /// </summary>
        /// <value>
        /// Řetězec obsahující název předmětu.
        /// </value>
        public string Nazev { get; set; }

        /// <summary>
        /// Typ předmětu, určuje, zda se jedná o teorii nebo cvičení.
        /// </summary>
        /// <value>
        /// Enum TypPredmetu určující typ předmětu.
        /// </value>
        public TypPredmetu Typ { get; set; }

        /// <summary>
        /// Učitel, který předmět vyučuje.
        /// </summary>
        /// <value>
        /// Instance třídy Ucitel.
        /// </value>
        public Ucitel Ucitel { get; set; }

        /// <summary>
        /// Učebna, ve které se předmět vyučuje.
        /// </summary>
        /// <value>
        /// Instance třídy Ucebna.
        /// </value>
        public Ucebna Ucebna { get; set; }

        /// <summary>
        /// Počet výskytů předmětu v rozvrhu.
        /// </summary>
        /// <value>
        /// Celé číslo reprezentující počet výskytů.
        /// </value>
        public int PocetVyskytu { get; set; }

        /// <summary>
        /// Konstruktor třídy Predmet.
        /// </summary>
        /// <param name="nazev">Název předmětu.</param>
        /// <param name="typ">Typ předmětu (teorie/cvičení).</param>
        /// <param name="ucitel">Učitel vyučující předmět.</param>
        /// <param name="ucebna">Učebna, ve které se předmět vyučuje.</param>
        /// <param name="pocetVyskytu">Počet výskytů předmětu v rozvrhu.</param>
        public Predmet(string nazev, TypPredmetu typ, Ucitel ucitel, Ucebna ucebna, int pocetVyskytu)
        {
            Nazev = nazev;
            Typ = typ;
            Ucitel = ucitel;
            Ucebna = ucebna;
            PocetVyskytu = pocetVyskytu;
        }
    }
}
