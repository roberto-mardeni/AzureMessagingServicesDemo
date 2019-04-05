using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Threading.Tasks;

namespace StorageAccountQueueSender
{
    internal class Program
    {
        private static CloudQueue queue;

        private static void Main(string[] args)
        {
            Console.WriteLine("Azure Storage Queues - Sender");
            Console.WriteLine();

            ProcessAsync().GetAwaiter().GetResult();

            Console.WriteLine("Press any key to exit the sample application.");
            Console.ReadLine();
        }

        private static async Task ProcessAsync()
        {
            var config = new Configuration.Configuration();
            CloudStorageAccount storageAccount = null;

            // Retrieve the connection string for use with the application. The storage connection string is stored
            // in an environment variable called storageconnectionstring, on the machine where the application is running.
            // If the environment variable is created after the application is launched in a console or with Visual
            // Studio, the shell needs to be closed and reloaded to take the environment variable into account.
            string storageConnectionString = config.StorageAccountConnectionString;

            // Check whether the connection string can be parsed.
            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                try
                {
                    // Create the CloudQueueClient that represents the queue endpoint for the storage account.
                    var cloudQueueClient = storageAccount.CreateCloudQueueClient();

                    // Create a queue called 'quickstartqueues' and append a GUID value so that the queue name 
                    // is unique in your storage account. 
                    queue = cloudQueueClient.GetQueueReference(config.StorageAccountQueueName);
                    await queue.CreateIfNotExistsAsync();
                    Console.WriteLine("Created queue '{0}'", queue.Name);
                    Console.WriteLine();

                    SendMessagesAsync(config.MessageCount).GetAwaiter().GetResult();
                }
                catch (StorageException ex)
                {
                    Console.WriteLine("Error returned from Azure Storage: {0}", ex.Message);
                }
            }
            else
            {
                Console.WriteLine(
                    "A connection string has not been defined in the system environment variables. " +
                    "Add a environment variable named 'storageconnectionstring' with your storage " +
                    "connection string as a value.");
            }
        }

        private static async Task SendMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {
                for (var i = 0; i < numberOfMessagesToSend; i++)
                {
                    // Create a message and add it to the queue. Set expiration time to 7 days.
                    var message = new CloudQueueMessage($"Message {i}");
                    await queue.AddMessageAsync(message, new TimeSpan(7, 0, 0, 0), null, null, null);
                    Console.WriteLine("Added message '{0}' to queue '{1}'", message.Id, queue.Name);
                    Console.WriteLine("Message insertion time: {0}", message.InsertionTime.ToString());
                    Console.WriteLine("Message expiration time: {0}", message.ExpirationTime.ToString());
                    Console.WriteLine();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}
