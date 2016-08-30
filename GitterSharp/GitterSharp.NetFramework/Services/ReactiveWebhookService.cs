using System;
using GitterSharp.Model;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using System.Reactive.Linq;

namespace GitterSharp.Services
{
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
