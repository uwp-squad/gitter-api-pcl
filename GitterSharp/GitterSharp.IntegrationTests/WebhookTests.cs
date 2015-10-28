using GitterSharp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

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
            bool result = await webhookService.PostAsync("https://webhooks.gitter.im/e/cdf519d88a935d54a6d2", "A simple test");

            // Assert
            Assert.IsTrue(result);
        }
    }
}
