namespace MonitorIt.Core.DnsResolution
{
    /// <summary>
    /// The settings for configuring DNS resolution.
    /// </summary>
    public class DnsResolverSettings
    {
        /// <summary>
        /// If true, all V4 IP addresses will be returned in the DNS resolution request.
        /// </summary>
        public bool IncludeIpV4Addresses { get; set; } = true;

        /// <summary>
        /// If true, all V6 IP addresses will be returned in the DNS resolution request.
        /// </summary>
        public bool IncludeIpV6Addresses { get; set; } = true;
    }
}
