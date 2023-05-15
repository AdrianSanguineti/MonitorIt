namespace System
{
    using MonitorIt.Core;

    /// <summary>
    /// Internal extensions for <see cref="Uri"/>.
    /// </summary>
    internal static class UriExtensions
    {
        /// <summary>
        /// Create a <see cref="HttpRequestOptionsKey{TargetIpAddress}"/> for the request.
        /// </summary>
        /// <param name="request">The request uri.</param>
        /// <returns>A new <see cref="HttpRequestOptionsKey{TargetIpAddress}"/> instance for <paramref name="request"/>.</returns>
        internal static HttpRequestOptionsKey<TargetIpAddress> CreateHttpRequestOptionsKey(this Uri request)
        {
            return new HttpRequestOptionsKey<TargetIpAddress>(request.ToString());
        }
    }
}
