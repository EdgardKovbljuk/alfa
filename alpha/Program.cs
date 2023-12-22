using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace alpha
{
     class Program
    {

        public static int pocetOhodnocenychRozvrhu = 0;
        private static int pocetVygenerovanychRozvrhu = 0;

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var csvReader = new CsvReader();
            var predmety = csvReader.NacistPredmetyZCsv("../../../rozvrh.csv");

            int pocetGeneratoru = 3;
            int pocetHodnotitelu = 2;
            var generatoryTasks = new List<Task<IEnumerable<Rozvrh>>>();
            var hodnotiteleTasks = new List<Task<List<RozvrhHodnoceni>>>();

            using (var cts = new CancellationTokenSource())
            {
                cts.CancelAfter(TimeSpan.FromSeconds(10));

                for (int i = 0; i < pocetGeneratoru; i++)
                {
                    var generator = new RozvrhGenerator(predmety);
                    generatoryTasks.Add(generator.GenerovatRozvrhyAsync(cts.Token));
                }

                var vsechnyRozvrhy = await Task.WhenAll(generatoryTasks);
                var rozvrhyProHodnoceni = vsechnyRozvrhy.SelectMany(list => list).ToList();
                pocetVygenerovanychRozvrhu = rozvrhyProHodnoceni.Count;

                var rozdeleniRozvrhu = RozvrhUtils.RozdelitRozvrhy(rozvrhyProHodnoceni, pocetHodnotitelu);

                for (int i = 0; i < pocetHodnotitelu; i++)
                {
                    var hodnotitel = new Hodnotitel();
                    hodnotiteleTasks.Add(RozvrhHodnotitel.HodnotitRozvrhyAsync(hodnotitel, rozdeleniRozvrhu[i]));
                }

                await Task.WhenAll(hodnotiteleTasks);

                Console.WriteLine($"Celkový počet vygenerovaných rozvrhů: {pocetVygenerovanychRozvrhu}");
                Console.WriteLine($"Celkový počet ohodnocených rozvrhů: {pocetOhodnocenychRozvrhu}");

                var nejlepsiRozvrhy = RozvrhUtils.VybratNejlepsiRozvrhy(hodnotiteleTasks, 5);
                RozvrhUtils.UlozitRozvrhyDoSouboru(nejlepsiRozvrhy, "../../../nejlepsiRozvrhy.txt");
            }
        }
    }
}
