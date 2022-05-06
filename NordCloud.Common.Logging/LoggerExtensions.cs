﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NordCloud.Logging
{
    public static class LoggerExtensions
    {
        public static void LogHttpResponse(this ILogger logger, HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                logger.LogDebug("Received a success response from {Url}", response.RequestMessage.RequestUri);
            }
            else
            {
                logger.LogWarning("Received a non-success status code {StatusCode} from {Url}",
                    (int)response.StatusCode, response.RequestMessage.RequestUri);
            }
        }
    }
}
