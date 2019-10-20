﻿using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Application.Infrastructure {
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public RequestPerformanceBehaviour (ILogger<TRequest> logger) {
            _timer = new Stopwatch ();

            _logger = logger;

        }

        public async Task<TResponse> Handle (TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {
            _timer.Start ();

            var response = await next ();

            _timer.Stop ();

            if (_timer.ElapsedMilliseconds > 500) {
                var name = typeof (TRequest).Name;

                _logger.LogWarning ("Application Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                    name, _timer.ElapsedMilliseconds, request);
            }

            return response;
        }
    }
}