using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WorkerService1.Helpers;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                try
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(EnvironmentVariable.Domain(_configuration));
                    HttpResponseMessage response = await client.GetAsync(EnvironmentVariable.ServiceUrl(_configuration));
                    _logger.LogInformation("Response: {statusCode}", response.StatusCode.ToString());
                }
                catch(Exception ex)
                {
                    _logger.LogInformation("Error When Call Service | Exception: "+ex.ToString());
                }
                await Task.Delay(EnvironmentVariable.Interval(_configuration), stoppingToken);
            }
        }
    }
}
