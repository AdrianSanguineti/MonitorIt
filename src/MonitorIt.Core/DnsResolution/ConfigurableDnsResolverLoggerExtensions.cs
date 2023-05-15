namespace MonitorIt.Core.DnsResolution
{
    using Microsoft.Extensions.Logging;
    using System.Net.Sockets;
    using System.Net;

    /// <summary>
    /// Logging extensions for the <see cref="FilterableDnsResolver"/> class.
    /// </summary>
    internal static partial class ConfigurableDnsResolverLoggerExtensions
    {
        private static Func<ILogger, string, IDisposable?> dnsResolutionScope = LoggerMessage.DefineScope<string>("Resolving IP addresses for {hostName}.");

        public static IDisposable? DnsResolutionScope(this ILogger logger, string hostName)
        {
            return dnsResolutionScope(logger, hostName);
        }

        [LoggerMessage(EventId = 50, Level = LogLevel.Warning, Message = "The IP Address '{address}' for the hostname: {hostName} has an unsupported address family type: {addressFamily}.")]
        public static partial void UnsupportedAddressFamily(this ILogger<FilterableDnsResolver> logger, IPAddress address, string hostName, AddressFamily addressFamily);

        [LoggerMessage(EventId = 51, Level = LogLevel.Debug, Message = "The IP Address '{address}' has been excluded as IPV4 addresses have been disabled.")]
        public static partial void IpAddressV4Excluded(this ILogger<FilterableDnsResolver> logger, IPAddress address);

        [LoggerMessage(EventId = 52, Level = LogLevel.Debug, Message = "The IP Address '{address}' has been excluded as IPV6 addresses have been disabled.")]
        public static partial void IpAddressV6Excluded(this ILogger<FilterableDnsResolver> logger, IPAddress address);
    }
}
