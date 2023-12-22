using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alpha.Tests
{
    public class RozvrhGeneratorTests
    {
        [Fact]
        public async Task GenerovatRozvrhyAsync_GenerRozvrhyCasovemLimit()
        {
            
            var predmety = new List<Predmet>
            {
                new Predmet("M", TypPredmetu.Teorie, new Ucitel("Novak"), new Ucebna("15501"), 2),
                new Predmet("C", TypPredmetu.Teorie, new Ucitel(" Modry"), new Ucebna("525"), 3),
                new Predmet("T", TypPredmetu.Teorie, new Ucitel("Zelenu"), new Ucebna("252"), 3),
                new Predmet("E", TypPredmetu.Teorie, new Ucitel("Prochazka"), new Ucebna("444"), 3),
                new Predmet("W", TypPredmetu.Teorie, new Ucitel("Novotny"), new Ucebna("15022"), 3)

            };

            var generator = new RozvrhGenerator(predmety);
            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5)); // Nastavte časový limit

            // Act
            var rozvrhy = await generator.GenerovatRozvrhyAsync(cancellationTokenSource.Token);

            // Assert
            // Zde můžete prověřit očekávané vlastnosti vygenerovaných rozvrhů, například jejich počet.
            Assert.NotEmpty(rozvrhy);
            Assert.All(rozvrhy, rozvrh =>
            {
                Assert.NotNull(rozvrh);
                Assert.Equal(5, rozvrh.Hodiny.GetLength(0)); // Očekáváme 5 dnů
                Assert.Equal(10, rozvrh.Hodiny.GetLength(1)); // Očekáváme 10 hodin denně
            });
        }

        

        [Fact]
        public void VypsatRozvrhy_RozvrhyDoConsole()
        {
            // Arrange
            var predmety = new List<Predmet>
            {
                new Predmet("Matematika", TypPredmetu.Teorie, new Ucitel("Mr. Smith"), new Ucebna("101"), 2),
                new Predmet("Fyzika", TypPredmetu.Teorie, new Ucitel("Mrs. Johnson"), new Ucebna("102"), 3)
                
            };

            var generator = new RozvrhGenerator(predmety);
            var rozvrhy = new List<Rozvrh>
            {
                new Rozvrh(5, 10), // Rozvrh 1
                new Rozvrh(5, 10) // Rozvrh 2
                
            };

            
            generator.VypsatRozvrhy(rozvrhy);
            
        }
    }
}