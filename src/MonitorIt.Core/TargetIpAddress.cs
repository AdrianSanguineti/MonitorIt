namespace MonitorIt.Core
{
    using System;
    using System.Net;

    /// <summary>
    /// Defines a relationship between a particular request <see cref="Uri"/> and the
    /// <see cref="IPAddress"/> that should be connected to evaluate the request against.
    /// </summary>
    public struct TargetIpAddress
    {
        public TargetIpAddress(IPAddress ipAddress, Uri request)
        {
            IPAddress = ipAddress ?? throw new ArgumentNullException(nameof(ipAddress));
            Request = request;
        }

        /// <summary>
        /// The target IP address.
        /// </summary>
        public IPAddress IPAddress { get; }

        /// <summary>
        /// The request.
        /// </summary>
        public Uri Request { get; }

        /// <summary>
        /// The host associated with the <see cref="Request"/>.
        /// </summary>
        public string Host => Request.Host;
    }
}
