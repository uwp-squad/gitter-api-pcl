using System;
using System.Reactive.Threading.Tasks;
using System.Reactive.Linq;
using GitterSharp.Model.Webhook;

namespace GitterSharp.Services
{
    public interface IReactiveWebhookService
    {
        /// <summary>
        /// Send event message to a dedicated room
        /// </summary>
        /// <param name="url">The webhook url used to send data</param>
        /// <param name="message">Content of the event message</param>
        /// <param name="level">Level of the message</param>
        /// <returns></returns>
        IObservable<bool> Post(string url, string message, MessageLevel level = MessageLevel.Info);
    }

    public class ReactiveWebhookService : IReactiveWebhookService
    {
        #region Fields

        private IWebhookService _webhookService = new WebhookService();

        #endregion

        #region Methods

        public IObservable<bool> Post(string url, string message, MessageLevel level = MessageLevel.Info)
        {
            return _webhookService.PostAsync(url, message, level).ToObservable();
        }

        #endregion
    }
}
