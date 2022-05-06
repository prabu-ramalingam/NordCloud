using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NordCloud.Logging
{
    public class LoggingDelegatingHandler : DelegatingHandler
    {
        private readonly ILogger<LoggingDelegatingHandler> logger;

        public LoggingDelegatingHandler(ILogger<LoggingDelegatingHandler> logger)
        {
            this.logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await base.SendAsync(request, cancellationToken);

                logger.LogHttpResponse(response);

                return response;
            }
            catch (HttpRequestException ex) when (ex.InnerException is SocketException se && se.SocketErrorCode == SocketError.ConnectionRefused)
            {
                var hostWithPort = "";

                logger.LogCritical(ex, "Unable to connect to {Host}.", hostWithPort);
            }

            return new HttpResponseMessage(HttpStatusCode.BadGateway)
            {
                RequestMessage = request
            };
        }
    }
}
