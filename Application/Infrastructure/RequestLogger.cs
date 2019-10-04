using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Northwind.Application.Interfaces;

namespace Application.Infrastructure {
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest> {
        private readonly ILogger _logger;

        public RequestLogger (ILogger<TRequest> logger) {
            _logger = logger;

        }

        public Task Process (TRequest request, CancellationToken cancellationToken) {
            var name = typeof (TRequest).Name;
            // TODO: Add User Details
            _logger.LogInformation ("Application Request: {Name} {@Request}",
                name, request);

            return Task.CompletedTask;
        }
    }
}