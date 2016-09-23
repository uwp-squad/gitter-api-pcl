using GitterSharp.Services;
using GitterSharp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using GitterSharp.Model.Webhook;

namespace GitterSharp.IntegrationTests
{
    [TestClass]
    public class WebhookTests
    {
        [TestMethod]
        public async Task Can_Post_Simple_Message()
        {
            // Arrange
            IWebhookService webhookService = new WebhookService();

            // Act
            bool result = await webhookService.PostAsync("https://webhooks.gitter.im/e/cdf519d88a935d54a6d2", "A simple message");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Can_Post_Simple_Error_Message()
        {
            // Arrange
            IWebhookService webhookService = new WebhookService();

            // Act
            bool result = await webhookService.PostAsync("https://webhooks.gitter.im/e/cdf519d88a935d54a6d2", "A simple error message", MessageLevel.Error);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
