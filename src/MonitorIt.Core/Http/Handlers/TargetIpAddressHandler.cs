namespace MonitorIt.Core.Http.Handlers
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// A custom <see cref="DelegatingHandler"/> which will execute the <see cref="HttpRequestMessage"/> against a specific pre-configure
    /// <see cref="TargetIpAddress"/>, instead of using the default IP resolution logic.
    /// </summary>
    public class TargetIpAddressHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Check if the current request has a TargetIpAddress configured for it.
            if (request.RequestUri != null
                && request.Options.TryGetValue(request.RequestUri.CreateHttpRequestOptionsKey(), out TargetIpAddress targetIpAddress))
            {
                // Change the request to use the requested target IP address.
                var builder = new UriBuilder(request.RequestUri)
                {
                    Host = targetIpAddress.IPAddress.ToString()
                };

                // To make the request valid again, update the request to set the original host as a HTTP header so that the upstream
                // server knows which site is to be served for the IP address. (Especially critical for servers which host multiple
                // sites on the same IP address).
                request.RequestUri = builder.Uri;
                request.Headers.TryAddWithoutValidation("host", targetIpAddress.Host);
            }

            // Execute the request.
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
