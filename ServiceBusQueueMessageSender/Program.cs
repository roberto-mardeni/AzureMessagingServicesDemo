﻿using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusQueueMessageSender
{
    internal class Program
    {
        private static IQueueClient queueClient;

        private static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();

            Console.WriteLine("Sent Message!!!");
        }

        private static async Task MainAsync()
        {
            var config = new Configuration.Configuration();

            queueClient = new QueueClient(config.ServiceBusNamespaceConnectionString, config.ServiceBusQueueName);

            Console.WriteLine("======================================================");
            Console.WriteLine("Press ENTER key to exit after sending all the messages.");
            Console.WriteLine("======================================================");

            // Send messages.
            await SendMessagesAsync(config.MessageCount);

            Console.ReadKey();

            await queueClient.CloseAsync();
        }

        private static async Task SendMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {
                for (var i = 0; i < numberOfMessagesToSend; i++)
                {
                    // Create a new message to send to the queue.
                    string messageBody = $"Message {i}";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    // Write the body of the message to the console.
                    Console.WriteLine($"Sending message: {messageBody}");

                    // Send the message to the queue.
                    await queueClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}
