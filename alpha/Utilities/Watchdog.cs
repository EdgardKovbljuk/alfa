using System;
using System.Threading;

namespace alpha
{
    /// <summary>
    /// Třída Watchdog slouží k monitorování a správě časového limitu určité operace.
    /// </summary>
    public class Watchdog
    {
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// Inicializuje novou instanci třídy Watchdog.
        /// </summary>
        public Watchdog()
        {
            cancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// Spustí watchdog s nastaveným časovým limitem.
        /// </summary>
        /// <param name="timeout">Časový limit, po jehož uplynutí bude vyvolána akce přerušení.</param>
        /// <remarks>
        /// Po uplynutí časového limitu bude CancellationToken vlastnost Token nastaven na přerušený stav.
        /// </remarks>
        public void Start(TimeSpan timeout)
        {
            cancellationTokenSource.CancelAfter(timeout);
        }

        /// <summary>
        /// Získává token přerušení, který lze použít pro sledování stavu timeoutu.
        /// </summary>
        public CancellationToken Token => cancellationTokenSource.Token;

        /// <summary>
        /// Zastaví watchdog a zruší požadavek na přerušení.
        /// </summary>
        /// <remarks>
        /// Tato metoda zastaví časovač a zruší všechny čekající akce přerušení.
        /// </remarks>
        public void Stop()
        {
            cancellationTokenSource.Cancel();
        }
    }
}
