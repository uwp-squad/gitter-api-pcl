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
    IObservable<IEnumerable<Room>> GetSuggestedRooms();

    #endregion

    #region Unread Items

    IObservable<UnreadItems> RetrieveUnreadChatMessages(string userId, string roomId);
    IObservable<Unit> MarkUnreadChatMessages(string userId, string roomId, IEnumerable<string> messageIds);

    #endregion

    #region Rooms

    IObservable<IEnumerable<Room>> GetRooms();
    IObservable<IEnumerable<User>> GetRoomUsers(string roomId, int limit = 30, string q = null, int skip = 0);
    IObservable<Room> JoinRoom(string roomName);
    IObservable<Room> JoinRoom(string userId, string roomId);
    IObservable<Room> UpdateRoom(string roomId, UpdateRoomRequest request);
    IObservable<bool> UpdateUserRoomSettings(string userId, string roomId, UpdateUserRoomSettingsRequest request);
    IObservable<RoomNotificationSettingsResponse> UpdateRoomNotificationSettings(string userId, string roomId, UpdateRoomNotificationSettingsRequest request);
    IObservable<SuccessResponse> LeaveRoom(string roomId, string userId);
    IObservable<SuccessResponse> DeleteRoom(string roomId);
    IObservable<IEnumerable<Room>> GetSuggestedRooms(string roomId);
    IObservable<IEnumerable<Collaborator>> GetSuggestedCollaboratorsOnRoom(string roomId);
    IObservable<IEnumerable<RoomIssue>> GetRoomIssues(string roomId);
    IObservable<IEnumerable<Ban>> GetRoomBans(string roomId);
    IObservable<WelcomeMessage> GetWelcomeMessage(string roomId);
    IObservable<UpdateWelcomeMessageResponse> UpdateWelcomeMessage(string roomId, UpdateWelcomeMessageRequest request);

    #endregion

    #region Messages

    IObservable<Message> GetSingleRoomMessage(string roomId, string messageId);
    IObservable<IEnumerable<Message>> GetRoomMessages(string roomId, MessageRequest request);
    IObservable<Message> SendMessage(string roomId, string message);
    IObservable<Message> UpdateMessage(string roomId, string messageId, string message);

    #endregion

    #region Events

    IObservable<IEnumerable<RoomEvent>> GetRoomEvents(string roomId);

    #endregion

    #region Groups

    IObservable<IEnumerable<Group>> GetGroups();
    IObservable<IEnumerable<Room>> GetGroupRooms(string groupId);
    IObservable<Room> CreateRoom(string groupId, CreateRoomRequest request);

    #endregion

    #region Search

    IObservable<SearchResponse<Room>> SearchRooms(string query, int limit = 10);
    IObservable<SearchResponse<User>> SearchUsers(string query, int limit = 10);
    IObservable<SearchResponse<Repository>> SearchUserRepositories(string userId, string query, int limit = 10);

    #endregion

    #region Streaming

    IObservable<Message> GetRealtimeMessages(string roomId);
    IObservable<RoomEvent> GetRealtimeEvents(string roomId);

    #endregion

    #region Analytics

    IObservable<Dictionary<DateTime, int>> GetRoomMessagesCountByDay(string roomId);

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

Retrieve realtime new events inside a room.

```
gitterApiService.GetRealtimeEvents(Room.Id)
                .Subscribe(event => 
					{
						// Do everything you need
						// This code will be executed each time the room receive a new event
					});
```