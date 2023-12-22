using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;

namespace alpha
{
    /// <summary>
    /// Třída pro čtení dat o předmětech z CSV souboru.
    /// </summary>
    public class CsvReader
    {
        /// <summary>
        /// Načte předměty z CSV souboru.
        /// </summary>
        /// <param name="cestaKSouboru">Cesta k CSV souboru s daty o předmětech.</param>
        /// <returns>Seznam předmětů načtených z CSV souboru.</returns>
        public List<Predmet> NacistPredmetyZCsv(string cestaKSouboru)
        {
            var predmety = new List<Predmet>();

            try
            {
                using (TextFieldParser parser = new TextFieldParser(cestaKSouboru))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    parser.ReadLine(); // Přeskočení hlavičky

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        if (fields.Length >= 5 && int.TryParse(fields[4], out int pocetVyskytu))
                        {
                            TypPredmetu typ = fields[1].ToLower() == "teorie" ? TypPredmetu.Teorie : TypPredmetu.Cviceni;

                            var predmet = new Predmet(
                                fields[0],
                                typ, // Použití výčtového typu
                                new Ucitel(fields[2]),
                                new Ucebna(fields[3]),
                                pocetVyskytu
                            );
                            predmety.Add(predmet);
                        }
                        else
                        {
                            Console.WriteLine("Neplatný řádek v CSV souboru: ");
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                throw new Exception("Soubor nebyl nalezen: " + cestaKSouboru);
            }
            catch (IOException e)
            {
                throw new Exception("Došlo k chybě při čtení souboru: " + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("Došlo k neznámé chybě: " + e.Message);
            }

            return predmety;
        }
    }
}
