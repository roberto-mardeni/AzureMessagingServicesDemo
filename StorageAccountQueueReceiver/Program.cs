using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Threading.Tasks;

namespace StorageAccountQueueReceiver
{
    internal class Program
    {
        private static CloudQueue queue = null;

        private static void Main(string[] args)
        {
            Console.WriteLine("Azure Storage Queues - Receiver");
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
                    CloudQueueClient cloudQueueClient = storageAccount.CreateCloudQueueClient();

                    // Create a queue called 'quickstartqueues' and append a GUID value so that the queue name 
                    // is unique in your storage account. 
                    queue = cloudQueueClient.GetQueueReference(config.StorageAccountQueueName);

                    do
                    {
                        //// Peek at the message at the front of the queue. Peeking does not alter the message's 
                        //// visibility, so that another client can still retrieve and process it. 
                        //CloudQueueMessage peekedMessage = await queue.PeekMessageAsync();

                        //// Display the ID and contents of the peeked message.
                        //Console.WriteLine("Contents of peeked message '{0}': {1}", peekedMessage.Id, peekedMessage.AsString);
                        //Console.WriteLine();

                        // Retrieve the message at the front of the queue. The message becomes invisible for 
                        // a specified interval, during which the client attempts to process it.
                        CloudQueueMessage retrievedMessage = await queue.GetMessageAsync();

                        // Display the time at which the message will become visible again if it is not deleted.
                        Console.WriteLine("Message '{0}': '{1}' becomes visible again at {2}", retrievedMessage.Id, retrievedMessage.AsString, retrievedMessage.NextVisibleTime);
                        Console.WriteLine();

                        //Process and delete the message within the period of invisibility.
                        await queue.DeleteMessageAsync(retrievedMessage);
                        Console.WriteLine("Processed and deleted message '{0}'", retrievedMessage.Id);
                        Console.WriteLine();
                    } while (true);
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
    }
}
