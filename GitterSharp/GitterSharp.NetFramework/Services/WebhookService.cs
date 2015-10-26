using System;
using System.Threading.Tasks;
using GitterSharp.Model;

namespace GitterSharp.Services
{
    public class WebhookService : IWebhookService
    {
        public Task<bool> Post(string url, string content, MessageLevel level = MessageLevel.Info)
        {
            throw new NotImplementedException();
        }
    }
}
