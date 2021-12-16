namespace CQRS.Events
{
    using System.Threading;
    using System.Threading.Tasks;

    using CQRS.Services;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class OperationsHandler : BackgroundService
    {
        private readonly ILogger<OperationsHandler> logger;
        private readonly ICommandService service;

        public OperationsHandler(ILogger<OperationsHandler> logger, ICommandService service)
        {
            this.logger = logger;
            this.service = service;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug("StartAsync");
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogDebug("ExecuteAsync");
            throw new System.NotImplementedException();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug("StopAsync");
            return base.StartAsync(cancellationToken);
        }
    }
}
