using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace MyConsole {
    public class MyService {
        readonly ILogger<MyService> _logger;
        public MyService(ILogger<MyService> logger) {
            _logger = logger;
        }

        public void Start() {
            _logger.LogInformation("MyService ...");
        }
    }

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

    class Program {
        static async Task Main(string[] args) {
            var host = new HostBuilder()
                .ConfigureLogging(builder => {
                    builder.AddConsole();
                })
                .ConfigureServices((context, services) => {
                    services.AddSingleton<MyService>();
                    services.Configure<HostOptions>(options => {
                        options.ShutdownTimeout = TimeSpan.FromSeconds(20);
                    });
                    services.AddHostedService<LifetimeEventsHostedService>();
                })
                .UseConsoleLifetime()
                .Build();

            await host.RunAsync();
        }
    }
}
