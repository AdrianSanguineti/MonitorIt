namespace MonitorIt.Core.Http
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Settings for the configuring the endpoint testing behaviour.
    /// </summary>
    public class HttpEndpointTestSettings
    {
        /// <summary>
        /// The number of milliseconds to wait before the entire execution times out.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Timeout { get; set; } = 5000;

        /// <summary>
        /// The number of milliseconds to wait before timing out a connection attempt.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int ConnectionTimeOut { get; set; } = 1000;
    }
}
