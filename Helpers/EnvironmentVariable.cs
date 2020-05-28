using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerService1.Helpers
{
    public static class EnvironmentVariable
    {
        public static int Interval(IConfiguration configuration)
        {
            return configuration.GetValue<int>($"Interval");
        }
        public static string Domain(IConfiguration configuration)
        {
            return configuration.GetValue<string>($"Domain");
        }
        public static string ServiceUrl(IConfiguration configuration)
        {
            return configuration.GetValue<string>($"ServiceUrl");
        }
    }
}
