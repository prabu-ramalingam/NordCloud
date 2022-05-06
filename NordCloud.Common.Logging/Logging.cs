using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCloud.Logging
{
    public static class Logging
    {

        public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger =>
          (hostingContext, loggerConfiguration) =>
          {
              var env = hostingContext.HostingEnvironment;             

              //TODO Enrich

              if (hostingContext.HostingEnvironment.IsDevelopment())
              {
                  loggerConfiguration.MinimumLevel.Override("NordCloud", LogEventLevel.Debug);
              }

              var elasticUrl = ""; //TODO From config

              if (!string.IsNullOrEmpty(elasticUrl))
              {
                  loggerConfiguration.WriteTo.Elasticsearch(
                      new ElasticsearchSinkOptions(new Uri(elasticUrl))
                      {
                          AutoRegisterTemplate = true,
                          AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                          IndexFormat = "nordcloud-logs-{0:yyyy.MM.dd}",
                          MinimumLogEventLevel = LogEventLevel.Debug
                      });
              }
          };
    }
}
