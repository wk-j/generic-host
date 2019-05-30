using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace MyConsole {
    public class LifetimeEventsHostedService : IHostedService {
        private readonly MyService _service;
        private readonly ILogger<LifetimeEventsHostedService> _logger;

        public LifetimeEventsHostedService(MyService service, ILogger<LifetimeEventsHostedService> logger) {
            _service = service;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            _logger.LogInformation("LifetimeEventsHostedService ...");
            _service.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            _logger.LogInformation("Stop ...");
            return Task.CompletedTask;
        }
    }
}
