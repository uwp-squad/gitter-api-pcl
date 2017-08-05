using Bayeux;
using Bayeux.Diagnostics;
using GitterSharp.Configuration;
using GitterSharp.Model.Realtime;
using GitterSharp.Realtime;
using Newtonsoft.Json;
using System;
using System.Reactive.Linq;

namespace GitterSharp.Services
{
    public interface IRealtimeGitterService
    {
        /// <summary>
        /// Connect to faye pub-sub system
        /// </summary>
        void Connect();

        /// <summary>
        /// Disconnect from faye pub-sub system
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Subscribe to get user presence (on or off) of a romm in realtime
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        IObservable<RealtimeUserPresence> SubscribeToUserPresence(string roomId);

        /// <summary>
        /// Subscribe to get chat messages of a romm in realtime (added, updated or removed)
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        IObservable<RealtimeChatMessage> SubscribeToChatMessages(string roomId);

        /// <summary>
        /// Subscribe to get users of a romm in realtime (added or removed)
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        IObservable<RealtimeRoomUser> SubscribeToRoomUsers(string roomId);

        /// <summary>
        /// Subscribe to get events of a romm in realtime
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        IObservable<RealtimeRoomEvent> SubscribeToRoomEvents(string roomId);

        /// <summary>
        /// Subscribe to get info of why read a message in a romm in realtime
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="messageId">Id of the message</param>
        /// <returns></returns>
        IObservable<RealtimeReadBy> SubscribeToChatMessagesReadBy(string roomId, string messageId);
    }

    public class RealtimeGitterService : IRealtimeGitterService
    {
        #region Fields

        private BayeuxClient _client;

        #endregion

        #region Constructor

        public RealtimeGitterService(string token)
        {
            // Create the client
            var endpoint = new Uri(Constants.FayeBaseUrl);
            var settings = new BayeuxClientSettings(endpoint)
            {
                Logger = new ConsoleLogger()
            };
            settings.Extensions.Add(new TokenBayeuxProtocolExtension { Token = token });

            _client = new BayeuxClient(settings);
        }

        #endregion

        #region Methods

        public void Connect()
        {
            _client.Connect();
        }

        public void Disconnect()
        {
            _client.Disconnect();
        }

        public IObservable<RealtimeUserPresence> SubscribeToUserPresence(string roomId)
        {
            return Observable.Create<RealtimeUserPresence>(o =>
            {
                _client.Subscribe($"/api/v1/rooms/{roomId}", message =>
                {
                    try
                    {
                        var realtimeUserPresence = JsonConvert.DeserializeObject<RealtimeUserPresence>(message.Data.ToString());
                        o.OnNext(realtimeUserPresence);
                    }
                    catch (Exception e)
                    {
                        var exception = new Exception($"Cannot create object from response {message.Data.ToString()}", e);
                        o.OnError(exception);
                    }
                });

                return () => { };
            });
        }

        public IObservable<RealtimeChatMessage> SubscribeToChatMessages(string roomId)
        {
            return Observable.Create<RealtimeChatMessage>(o =>
            {
                _client.Subscribe($"/api/v1/rooms/{roomId}/chatMessages", message =>
                {
                    try
                    {
                        var realtimeChatMessage = JsonConvert.DeserializeObject<RealtimeChatMessage>(message.Data.ToString());
                        o.OnNext(realtimeChatMessage);
                    }
                    catch (Exception e)
                    {
                        var exception = new Exception($"Cannot create object from response {message.Data.ToString()}", e);
                        o.OnError(exception);
                    }
                });

                return () => { };
            });
        }

        public IObservable<RealtimeRoomUser> SubscribeToRoomUsers(string roomId)
        {
            return Observable.Create<RealtimeRoomUser>(o =>
            {
                _client.Subscribe($"/api/v1/rooms/{roomId}/users", message =>
                {
                    try
                    {
                        var realtimeRoomUser = JsonConvert.DeserializeObject<RealtimeRoomUser>(message.Data.ToString());
                        o.OnNext(realtimeRoomUser);
                    }
                    catch (Exception e)
                    {
                        var exception = new Exception($"Cannot create object from response {message.Data.ToString()}", e);
                        o.OnError(exception);
                    }
                });

                return () => { };
            });
        }

        public IObservable<RealtimeRoomEvent> SubscribeToRoomEvents(string roomId)
        {
            return Observable.Create<RealtimeRoomEvent>(o =>
            {
                _client.Subscribe($"/api/v1/rooms/{roomId}/events", message =>
                {
                    try
                    {
                        var realtimeRoomEvent = JsonConvert.DeserializeObject<RealtimeRoomEvent>(message.Data.ToString());
                        o.OnNext(realtimeRoomEvent);
                    }
                    catch (Exception e)
                    {
                        var exception = new Exception($"Cannot create object from response {message.Data.ToString()}", e);
                        o.OnError(exception);
                    }
                });

                return () => { };
            });
        }

        public IObservable<RealtimeReadBy> SubscribeToChatMessagesReadBy(string roomId, string messageId)
        {
            return Observable.Create<RealtimeReadBy>(o =>
            {
                _client.Subscribe($"/api/v1/rooms/{roomId}/chatMessages/{messageId}/readBy", message =>
                {
                    try
                    {
                        var realtimeReadBy = JsonConvert.DeserializeObject<RealtimeReadBy>(message.Data.ToString());
                        o.OnNext(realtimeReadBy);
                    }
                    catch (Exception e)
                    {
                        var exception = new Exception($"Cannot create object from response {message.Data.ToString()}", e);
                        o.OnError(exception);
                    }
                });

                return () => { };
            });
        }

        #endregion
    }
}
