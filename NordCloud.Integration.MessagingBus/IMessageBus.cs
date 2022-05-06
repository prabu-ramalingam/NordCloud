using NordCloud.Integration.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCloud.Integration.MessagingBus
{
    public interface IMessageBus
    {
        Task PublishMessage(MessageBase message, string topicName);
    }
}
