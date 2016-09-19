# Gitter API is also reactive

Like the usual Gitter Api Service, you can use the Gitter API using Reactive Extensions. This time, everything you need is inside a single place : *ReactiveGitterApiService*.

```
public interface IReactiveGitterApiService
{
	#region Properties

    string Token { get; set; }

    #endregion

    #region User

    IObservable<User> GetCurrentUser();
    IObservable<IEnumerable<Organization>> GetOrganizations(string userId);
    IObservable<IEnumerable<Repository>> GetRepositories(string userId);

    #endregion

    #region Unread Items

    IObservable<UnreadItems> RetrieveUnreadChatMessages(string userId, string roomId);
    IObservable<Unit> MarkUnreadChatMessages(string userId, string roomId, IEnumerable<string> messageIds);

    #endregion

    #region Rooms

    IObservable<IEnumerable<Room>> GetRooms();
	IObservable<IEnumerable<User>> GetRoomUsers(string roomId, int limit = 30, string q = null, int skip = 0);
    IObservable<Room> JoinRoom(string roomName);

    #endregion

    #region Messages

    IObservable<Message> GetSingleRoomMessage(string roomId, string messageId);
    IObservable<IEnumerable<Message>> GetRoomMessages(string roomId, int limit = 50, string beforeId = null, string afterId = null, int skip = 0);
    IObservable<Message> SendMessage(string roomId, string message);
    IObservable<Message> UpdateMessage(string roomId, string messageId, string message);

    #endregion

    #region Events

    IObservable<IEnumerable<RoomEvent>> GetRoomEvents(string roomId);

    #endregion

    #region Streaming

    IObservable<Message> GetRealtimeMessages(string roomId);

    #endregion
}
```

## Usual Methods

Most of methods here are just usual methods adapted to Rx (User, Unread Items, Rooms and Messages). To use these methods, you can refer to the [usual](/docs/usual.md) docs.

And, instead of executing methods asynchronouly (*Task*), you will subscribe to the result. For example :

```
gitterApiService.GetCurrentUser()
				.Subscribe(user =>
					{
						// Execute code
					});
```

## [Streaming API](https://developer.gitter.im/docs/streaming-api)

### Realtime messages

Retrieve realtime new messages inside a room.

```
gitterApiService.GetRealtimeMessages(Room.Id)
                .Subscribe(message => 
					{
						// Do everything you need
						// This code will be executed each time the room receive a new message
					});
```

### Realtime events

Not available...