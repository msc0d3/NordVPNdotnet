using System;
using NordVPNdotnet.Support.Wait;

namespace NordVPNdotnet.Support
{
    /// <summary>
    /// Provides the ability to wait for an arbitrary condition during test execution.
    /// </summary>
    /// <example>
    /// <code>
    /// IWait wait = new NordWait(IConnect, TimeSpan.FromSeconds(3))
    /// IConnect connect = wait.Until(wait => nord.WaitConnect(TimeSpan.FromSeconds(5)));
    /// </code>
    /// </example>
    public class NordWait: DefaultWait<IConnect>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebDriverWait"/> class.
        /// </summary>
        /// <param name="driver">The Nord instance used to wait.</param>
        /// <param name="timeout">The timeout value indicating how long to wait for the condition.</param>
        public NordWait(IConnect driver, TimeSpan timeout)
            : this(new SystemClock(), driver, timeout, DefaultSleepTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NordWait"/> class.
        /// </summary>
        /// <param name="clock">An object implementing the <see cref="IClock"/> interface used to determine when time has passed.</param>
        /// <param name="driver">The Nord instance used to wait.</param>
        /// <param name="timeout">The timeout value indicating how long to wait for the condition.</param>
        /// <param name="sleepInterval">A <see cref="TimeSpan"/> value indicating how often to check for the condition to be true.</param>
        public NordWait(IClock clock, IConnect driver, TimeSpan timeout, TimeSpan sleepInterval)
            : base(driver, clock)
        {
            this.Timeout = timeout;
            this.PollingInterval = sleepInterval;
            this.IgnoreExceptionTypes(typeof(Exception));
        }

        private static TimeSpan DefaultSleepTimeout
        {
            get { return TimeSpan.FromMilliseconds(500); }
        }
    }
}
