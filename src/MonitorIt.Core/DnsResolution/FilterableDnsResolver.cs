namespace MonitorIt.Core.DnsResolution
{
    using Microsoft.Extensions.Logging;
    using System.Net;

    /// <summary>
    /// A <see cref="IDnsResolver"/> implementation that supports removing addresses from the returned <see cref="IPHostEntry"/>.
    /// </summary>
    /// <remarks>
    /// For the most part, the in-built .NET DNS resolution implementation works great. However for the
    /// MonitoryIt library, we want to have control over the addresses we a given so that we can test
    /// connections to all addresses behind a hostname. 
    /// </remarks>
    public class FilterableDnsResolver : IDnsResolver
    {
        private readonly ILogger<FilterableDnsResolver> logger;
        private readonly DnsResolverSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterableDnsResolver"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        public FilterableDnsResolver(ILogger<FilterableDnsResolver> logger)
            : this (new DnsResolverSettings(), logger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterableDnsResolver"/> class.
        /// </summary>
        /// <param name="dnsResolverSettings">The DNS resolver settings.</param>
        /// <param name="logger">The logger instance.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public FilterableDnsResolver(DnsResolverSettings dnsResolverSettings, ILogger<FilterableDnsResolver> logger)
        {
            settings = dnsResolverSettings ?? throw new ArgumentNullException(nameof(dnsResolverSettings));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public virtual async Task<IPHostEntry> GetIpHostEntryAsync(string hostname, CancellationToken cancellationToken)
        {
            using var scope = logger.DnsResolutionScope(hostname);

            // Use the in-built .NET DNS resolution implementation to get the host entry information.
            var entry = await Dns.GetHostEntryAsync(hostname, cancellationToken);

            var filteredAddresses = new List<IPAddress>();

            // Filter the entries to only return the address types requested in the settings.
            foreach (var address in entry.AddressList)
            {
                switch (address.AddressFamily)
                {
                    case System.Net.Sockets.AddressFamily.InterNetwork:
                        if (settings.IncludeIpV4Addresses)
                        {
                            filteredAddresses.Add(address);
                            break;
                        }

                        logger.IpAddressV4Excluded(address);
                        break;
                    case System.Net.Sockets.AddressFamily.InterNetworkV6:
                        if (settings.IncludeIpV6Addresses)
                        {
                            filteredAddresses.Add(address);
                            break;
                        }

                        logger.IpAddressV6Excluded(address);
                        break;
                    default:
                        logger.UnsupportedAddressFamily(address, hostname, address.AddressFamily);
                        break;
                }
            }

            entry.AddressList = filteredAddresses.ToArray();

            return entry;
        }
    }
}
