using GitterSharp.Model.Webhook;
using GitterSharp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace GitterSharp.IntegrationTests
{
    [TestClass]
    public class ReactiveWebhookTests
    {
        [TestMethod]
        public async Task Can_Post_Simple_Message()
        {
            // Arrange
            IReactiveWebhookService reactiveWebhookService = new ReactiveWebhookService();

            // Act
            reactiveWebhookService.Post("https://webhooks.gitter.im/e/cdf519d88a935d54a6d2", "A simple message")
                .Subscribe((result) =>
                {
                    // Assert
                    Assert.IsTrue(result);
                },
                (error) =>
                {
                    // Assert
                    Assert.Fail();
                });

            await Task.Delay(5000);
        }

        [TestMethod]
        public async Task Can_Post_Simple_Error_Message()
        {
            // Arrange
            IReactiveWebhookService reactiveWebhookService = new ReactiveWebhookService();

            // Act
            reactiveWebhookService.Post("https://webhooks.gitter.im/e/cdf519d88a935d54a6d2", "A simple error message", MessageLevel.Error)
                .Subscribe((result) =>
                {
                    // Assert
                    Assert.IsTrue(result);
                },
                (error) =>
                {
                    // Assert
                    Assert.Fail();
                });

            await Task.Delay(5000);
        }
    }
}
