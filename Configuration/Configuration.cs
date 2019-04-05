using Microsoft.Extensions.Configuration;
using System.IO;

namespace Configuration
{
    public class Configuration
    {
        private IConfigurationRoot config;

        public Configuration()
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..");

            config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public string StorageAccountConnectionString
        {
            get { return config[ConfigurationKeys.StorageAccountConnectionString]; }
        }

        public string StorageAccountQueueName
        {
            get { return "my-queue"; }
        }

        public string ServiceBusNamespaceConnectionString
        {
            get { return config[ConfigurationKeys.ServiceBusNamespaceConnectionString]; }
        }

        public int MessageCount
        {
            get { return 50; }
        }

        public string ServiceBusQueueName
        {
            get { return "my-queue"; }
        }

        public string ServiceBusTopicName
        {
            get { return "my-topic"; }
        }
    }
}
