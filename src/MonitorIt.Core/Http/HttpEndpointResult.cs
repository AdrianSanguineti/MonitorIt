namespace MonitorIt.Core.Http
{
    public class HttpEndpointResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpEndpointResult"/> class.
        /// </summary>
        /// <param name="targetIpAddress">The target IP address.</param>
        /// <param name="response">The received HTTP response message.</param>
        public HttpEndpointResult(TargetIpAddress targetIpAddress, HttpResponseMessage response)
        {
            TargetIpAddress = targetIpAddress;
            Response = response;
        }

        /// <summary>
        /// The IP Address
        /// </summary>
        public TargetIpAddress TargetIpAddress { get; set; }

        /// <summary>
        /// The <see cref="HttpResponseMessage"/> received.
        /// </summary>
        public HttpResponseMessage Response { get; set; }

        /// <summary>
        /// If true, indicates the HTTP endpoint testing was successful.
        /// </summary>
        public bool IsSuccess => Response.IsSuccessStatusCode;
    }
}
