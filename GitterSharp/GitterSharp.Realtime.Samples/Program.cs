using GitterSharp.Services;
using Newtonsoft.Json;
using System;

namespace GitterSharp.Realtime.Samples
{
    public class Program
    {
        #region Fields

#if DEBUG
        private static string _token = Environment.GetEnvironmentVariable("GITTER_TOKEN", EnvironmentVariableTarget.Machine);
#else
        private static string _token = Environment.GetEnvironmentVariable("GITTER_TOKEN");
#endif
        private static string _roomId = "56312c8816b6c7089cb89e07";

        #endregion

        public static void Main(string[] args)
        {
            IRealtimeGitterService realtimeGitterService = new RealtimeGitterService(_token);

            realtimeGitterService.Connect();

            realtimeGitterService.SubscribeToChatMessages(_roomId)
                .Subscribe(message =>
                {
                    Console.WriteLine("Message received.");
                    Console.WriteLine(JsonConvert.SerializeObject(message));
                });

            Console.ReadKey(true);
        }
    }
}
