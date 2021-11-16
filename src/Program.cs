using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using System;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Examples.Console
{
    public class Program
    {
        private static readonly Meter _appMeter = new("MyCompany.MyProduct.MyApp", "1.0");
        private static readonly Counter<long> _myCounter = _appMeter.CreateCounter<long>("MyCounter");

        public static async Task Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Debug)
                        .AddConsole();
            });
            var logger = loggerFactory.CreateLogger<Program>();

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            using var provider = Sdk.CreateMeterProviderBuilder()
                .AddMeter(_appMeter.Name)
                .AddOtlpExporter().Build();
            
            logger.LogInformation("Running and emitting metrics...");
            while (true)
            {
                _myCounter.Add(RandomNumberGenerator.GetInt32(1, 30), new("name", "apple"), new("color", "red"));
                _myCounter.Add(RandomNumberGenerator.GetInt32(5, 50), new("name", "lemon"), new("color", "yellow"));

                logger.LogInformation("Pausing for 2 seconds for export to happen");
                await Task.Delay(2000);                
            }
        }
    }
}
