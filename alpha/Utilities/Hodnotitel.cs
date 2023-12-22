using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alpha
{
    /// <summary>
    /// Třída Hodnotitel poskytuje funkce pro hodnocení rozvrhů na základě specifických kritérií.
    /// </summary>
    public class Hodnotitel
    {
        /// <summary>
        /// Asynchronně hodnotí zadaný rozvrh podle předem stanovených kritérií.
        /// </summary>
        /// <param name="rozvrh">Rozvrh, který má být ohodnocen.</param>
        /// <returns>Vrací celkové skóre rozvrhu jako celé číslo.</returns>
        /// <exception cref="ArgumentNullException">Vyhazuje, když je rozvrh null.</exception>
        /// <exception cref="Exception">Vyhazuje obecnou výjimku, když dojde k chybě během hodnocení.</exception>
        public async Task<int> OhodnotRozvrhAsync(Rozvrh rozvrh)
        {
            if (rozvrh == null)
            {
                throw new ArgumentNullException(nameof(rozvrh), "Rozvrh nesmí být null.");
            }

            int skore = 0;
            try
            {
                // Kontrola první hodiny v každém dni
                for (int den = 0; den < 5; den++)
                {
                    var prvniHodina = rozvrh.Hodiny[den, 0]?.Predmet?.Nazev;
                    if (prvniHodina == "C" || prvniHodina == "M" || prvniHodina == "AM")
                    {
                        skore -= 10;
                    }

                    // Kontrola 9. a 10. hodiny
                    var predmetyNaKontrolu = new string[] { "C", "M", "CIT", "DS", "PSS", "PIS", "TP", "A", "TV", "PV", "AM", "WA" };
                    var devataHodina = rozvrh.Hodiny[den, 8]?.Predmet?.Nazev;
                    var desataHodina = rozvrh.Hodiny[den, 9]?.Predmet?.Nazev;
                    if (predmetyNaKontrolu.Contains(devataHodina) || predmetyNaKontrolu.Contains(desataHodina))
                    {
                        skore -= 30;
                    }

                    // Kontrola, zda dvě vedle sebe jdoucí hodiny jsou cvičení stejného předmětu
                    var cviceniPredmety = new string[] { "PV", "WA", "CIT", "PIS", "PSS", "DS" };
                    for (int hodina = 0; hodina < 9; hodina++) // Kontrola do 9. hodiny (index 8)
                    {
                        var aktualniHodina = rozvrh.Hodiny[den, hodina]?.Predmet;
                        var dalsiHodina = rozvrh.Hodiny[den, hodina + 1]?.Predmet;

                        if (aktualniHodina != null && dalsiHodina != null &&
                            aktualniHodina.Typ == TypPredmetu.Cviceni &&
                            dalsiHodina.Typ == TypPredmetu.Cviceni &&
                            aktualniHodina.Nazev == dalsiHodina.Nazev &&
                            cviceniPredmety.Contains(aktualniHodina.Nazev))
                        {
                            skore += 40;
                        }
                    }
                    // Kontrola, zda jdou za sebou dvě hodiny s předměty M, C nebo A
                    var predmetyNaOdecet = new string[] { "M", "C", "A" };
                    for (int hodina = 0; hodina < 9; hodina++) // Kontrola do 9. hodiny (index 8)
                    {
                        var aktualniHodina = rozvrh.Hodiny[den, hodina]?.Predmet?.Nazev;
                        var dalsiHodina = rozvrh.Hodiny[den, hodina + 1]?.Predmet?.Nazev;

                        if (predmetyNaOdecet.Contains(aktualniHodina) && aktualniHodina == dalsiHodina)
                        {
                            skore -= 15;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Došlo k chybě při hodnocení rozvrhu: " + e.Message);
            }

            return skore;
        }
    }
}
