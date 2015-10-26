using GitterSharp.Model;
using System.Threading.Tasks;

namespace GitterSharp.Services
{
    public interface IWebhookService
    {
        /// <summary>
        /// Send event message to a dedicated room
        /// </summary>
        /// <param name="url">The webhook url used to send data</param>
        /// <param name="content">Content of the event message</param>
        /// <param name="level">Level of the message</param>
        /// <returns></returns>
        Task<bool> Post(string url, string content, MessageLevel level = MessageLevel.Info);
    }
}
