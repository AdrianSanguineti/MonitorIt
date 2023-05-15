namespace MonitorIt.Core.Http
{
    using MonitorIt.Core.DnsResolution;

    public class HttpEndpointTester : IObservable<HttpEndpointResult>
    {
        private readonly IDnsResolver resolver;
        private readonly HttpClient httpClient;

        private List<IObserver<HttpEndpointResult>> observers;

        public HttpEndpointTester(IDnsResolver resolver, HttpClient httpClient, HttpEndpointTestSettings settings)
        {
            this.resolver = resolver;
            this.httpClient = httpClient;
            Settings = settings;
            observers = new List<IObserver<HttpEndpointResult>>();
        }

        public HttpEndpointTestSettings Settings { get; }

        /// <inheritdoc />
        //public async Task<TestRun<HttpEndpointResult>> ExecuteAsync(Uri endpoint, CancellationToken cancellationToken = default)
        //{
        //    var timeoutCancellationSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        //    timeoutCancellationSource.CancelAfter(Settings.Timeout);

        //    var hostEntry = await dnsResolver.GetHostEntryAsync(endpoint.Host, timeoutCancellationSource.Token);

        //    var connectionTests = new List<Task<HttpEndpointResult>>();
        //    foreach (var address in hostEntry.AddressList)
        //    {

        //        TargetIpAddress targetIpAddress = new()
        //        {
        //            Request = endpoint,
        //            IPAddress = address
        //        };

        //        connectionTests.Add(TestAddress(targetIpAddress, HttpMethod.Get));
        //    }

        //    await Task.WhenAll(connectionTests);

        //    return new TestRun<HttpEndpointResult>
        //    {
        //        TestResults = connectionTests.Select(x => x.Result).ToList()
        //    };
        //}

        public IDisposable Subscribe(IObserver<HttpEndpointResult> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }

            return new Unsubscriber(observers ,observer);
        }

        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<HttpEndpointResult>> observers;
            private readonly IObserver<HttpEndpointResult> observer;

            public Unsubscriber(List<IObserver<HttpEndpointResult>> observers, IObserver<HttpEndpointResult> observer)
            {
                this.observers = observers;
                this.observer = observer;
            }

            public void Dispose()
            {
                if (observer != null && observers.Contains(observer))
                    observers.Remove(observer);
            }
        }
    }
}
