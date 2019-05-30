using Microsoft.Extensions.Logging;

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
}
