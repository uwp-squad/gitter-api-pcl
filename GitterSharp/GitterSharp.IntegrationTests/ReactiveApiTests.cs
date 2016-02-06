using System;
using GitterSharp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace GitterSharp.IntegrationTests
{
    [TestClass]
    public class ReactiveApiTests
    {
        #region Fields

        private string _token = Environment.GetEnvironmentVariable("GITTER_TOKEN", EnvironmentVariableTarget.Machine);
        private string _roomId = "56312c8816b6c7089cb89e07";

        #endregion


        #region Methods

        [TestMethod]
        public async Task Can_Receive_Realtime_Messages()
        {
            // Arrange
            int messagesReceived = 0;
            IReactiveGitterApiService gitterApiService = new ReactiveGitterApiService(_token);

            // Act
            gitterApiService.GetRealtimeMessages(_roomId)
                    .Subscribe(message =>
                    {
                        messagesReceived++;
                    });

            await gitterApiService.SendMessage(_roomId, "Test");

            await Task.Delay(2000);

            // Assert
            Assert.AreEqual(1, messagesReceived);
        }

        #endregion
    }
}
