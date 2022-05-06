using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;
using NordCloud.Integration.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCloud.Integration.MessagingBus
{
    public  class AzServiceBusMessageBus:IMessageBus
    {

        //TODO: read from settings
        private string connectionString = "";

        public async Task PublishMessage(MessageBase message, string topicName)
        {
            ISenderClient topicClient = new TopicClient(connectionString, topicName);

            var jsonMessage = JsonConvert.SerializeObject(message);
            var serviceBusMessage = new Message(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };

            await topicClient.SendAsync(serviceBusMessage);
            Console.WriteLine($"Sent message to {topicClient.Path}");
            await topicClient.CloseAsync();

        }
    }
}
