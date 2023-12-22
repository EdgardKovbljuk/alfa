using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alpha.Tests
{
    public class HodnotitelTests
    {
        

        [Fact]
        public async Task OhodnotRozvrhAsync_NullRozvrh_ThrowsArgumentNullException()
        {
            // Arrange
            var hodnotitel = new Hodnotitel();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => hodnotitel.OhodnotRozvrhAsync(null));
        }

        
    }
}