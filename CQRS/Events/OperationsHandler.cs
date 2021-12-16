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
            logger.LogInformation("StartAsync");
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("ExecuteAsync");
            //throw new System.NotImplementedException();
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StopAsync");
            return base.StopAsync(cancellationToken);
        }
    }
}
