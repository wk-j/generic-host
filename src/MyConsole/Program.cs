using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MyConsole {

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
