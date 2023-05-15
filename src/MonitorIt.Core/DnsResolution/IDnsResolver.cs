namespace MonitorIt.Core.DnsResolution
{
    using System.Net;

    /// <summary>
    /// Defines methods for performing DNS resolution.
    /// </summary>
    public interface IDnsResolver
    {
        /// <summary>
        /// Gets the host entries for the specified hostname.
        /// </summary>
        /// <param name="hostname">The hostname, e.g. www.website.net</param>
        /// <param name="cancellationToken">A cancellation token for terminating the asynchronous request.</param>
        /// <returns>The a <see cref="IPHostEntry"/> instance containing the DNS information for the <paramref name="hostname"/>.</returns>
        Task<IPHostEntry> GetIpHostEntryAsync(string hostname, CancellationToken cancellationToken);
    }
}
