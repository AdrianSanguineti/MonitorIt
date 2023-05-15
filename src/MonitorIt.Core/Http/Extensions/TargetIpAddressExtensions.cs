namespace MonitorIt.Core.Http.Extensions
{
    using MonitorIt.Core.Http.Handlers;

    /// <summary>
    /// Extension methods for the <see cref="TargetIpAddress"/> class.
    /// </summary>
    public static class TargetIpAddressExtensions
    {
        /// <summary>
        /// Creates a new <see cref="HttpRequestMessage"/> setting the provided <see cref="TargetIpAddress"/> as the target for the
        /// request to be made against.
        /// 
        /// NOTE: Requires the <see cref="TargetIpAddressHandler"/> to be registered on the executing <see cref="HttpClient"/> for the
        /// <see cref="TargetIpAddress"/> to take affect.
        /// </summary>
        /// <param name="targetIpAddress">The target IP address details, including the request URI.</param>
        /// <returns>A new <see cref="HttpRequestMessage"/>, setup for the <see cref="TargetIpAddressHandler"/> to intercept and use the configured IP address.</returns>
        public static HttpRequestMessage CreateHttpRequestMessage(this TargetIpAddress targetIpAddress)
        {
            return CreateHttpRequestMessage(targetIpAddress, HttpMethod.Head);
        }

        /// <summary>
        /// Creates a new <see cref="HttpRequestMessage"/> setting the provided <see cref="TargetIpAddress"/> as the target for the
        /// request to be made against.
        /// 
        /// NOTE: Requires the <see cref="TargetIpAddressHandler"/> to be registered on the executing <see cref="HttpClient"/> for the
        /// <see cref="TargetIpAddress"/> to take affect.
        /// </summary>
        /// <param name="targetIpAddress">The target IP address details, including the request URI.</param>
        /// <param name="method">The HTTP method which the request message is initialised with.</param>
        /// <returns>A new <see cref="HttpRequestMessage"/>, setup for the <see cref="TargetIpAddressHandler"/> to intercept and use the configured IP address.</returns>
        public static HttpRequestMessage CreateHttpRequestMessage(this TargetIpAddress targetIpAddress, HttpMethod method)
        {
            HttpRequestMessage request = new(method, targetIpAddress.Request);

            // Set which IP Address we want the request to go to.
            request.Options.Set(targetIpAddress.Request.CreateHttpRequestOptionsKey(), targetIpAddress);

            return request;
        }
    }
}
