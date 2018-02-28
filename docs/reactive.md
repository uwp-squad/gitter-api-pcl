# Gitter API is also reactive

Like the usual Gitter Api Service, you can use the Gitter API using Reactive Extensions. This time, everything you need is inside a single place : *ReactiveGitterApiService*.

```c#
public interface IReactiveGitterApiService
{
	#region Properties

    string Token { get; set; }

    #endregion

    #region Repository

    IObservable<RepositoryInfo> GetRepositoryInfo(string repositoryName);

    #endregion

    #region User

    IObservable<GitterUser> GetCurrentUser();
    IObservable<IEnumerable<Organization>> GetMyOrganizations(bool unused = false);
    IObservable<IEnumerable<Organization>> GetOrganizations(string userId);
    IObservable<IEnumerable<Repository>> GetMyRepositories(string query, int limit = 0);
    IObservable<IEnumerable<Repository>> GetMyRepositories(bool unused = false);
    IObservable<IEnumerable<Repository>> GetRepositories(string userId);
    IObservable<IEnumerable<Room>> GetSuggestedRooms();
    IObservable<IEnumerable<RoomUnreadCount>> GetAggregatedUnreadItems();
    IObservable<UserInfo> GetUserInfo(string username);

    #endregion

    #region Unread Items

    IObservable<UnreadItems> RetrieveUnreadChatMessages(string userId, string roomId);
    IObservable<Unit> MarkUnreadChatMessages(string userId, string roomId, IEnumerable<string> messageIds);

    #endregion

    #region Rooms

    IObservable<IEnumerable<Room>> GetRooms();
    IObservable<IEnumerable<User>> GetRoomUsers(string roomId, int limit = 30, string q = null, int skip = 0);
    IObservable<Room> GetRoom(string roomName);
    IObservable<Room> JoinRoom(string userId, string roomId);
    IObservable<Room> UpdateRoom(string roomId, UpdateRoomRequest request);
    IObservable<bool> UpdateUserRoomSettings(string userId, string roomId, UpdateUserRoomSettingsRequest request);
    IObservable<RoomNotificationSettingsResponse> GetRoomNotificationSettings(string userId, string roomId);
    IObservable<RoomNotificationSettingsResponse> UpdateRoomNotificationSettings(string userId, string roomId, UpdateRoomNotificationSettingsRequest request);
    IObservable<SuccessResponse> LeaveRoom(string roomId, string userId);
    IObservable<SuccessResponse> DeleteRoom(string roomId);
    IObservable<IEnumerable<Room>> GetSuggestedRooms(string roomId);
    IObservable<IEnumerable<Collaborator>> GetSuggestedCollaboratorsOnRoom(string roomId);
    IObservable<IEnumerable<RoomIssue>> GetRoomIssues(string roomId);
    IObservable<IEnumerable<Ban>> GetRoomBans(string roomId);
    IObservable<BanUserResponse> BanUserFromRoom(string roomId, string username, bool removeMessages = false);
    IObservable<SuccessResponse> UnbanUser(string roomId, string userId);
    IObservable<WelcomeMessage> GetWelcomeMessage(string roomId);
    IObservable<UpdateWelcomeMessageResponse> UpdateWelcomeMessage(string roomId, UpdateWelcomeMessageRequest request);
    IObservable<InviteUserResponse> InviteUserInRoom(string roomId, InviteUserRequest request);

    #endregion

    #region Messages

    IObservable<Message> GetSingleRoomMessage(string roomId, string messageId);
    IObservable<IEnumerable<Message>> GetRoomMessages(string roomId, MessageRequest request);
    IObservable<Message> SendMessage(string roomId, string message);
    IObservable<Message> UpdateMessage(string roomId, string messageId, string message);
    IObservable<Unit> DeleteMessage(string roomId, string messageId);
    IObservable<IEnumerable<GitterUser>> GetUsersWhoReadMessage(string roomId, string messageId)

    #endregion

    #region Events

    IObservable<IEnumerable<RoomEvent>> GetRoomEvents(string roomId, int limit = 50, int skip = 0, string beforeId = null);

    #endregion

    #region Groups

    IObservable<IEnumerable<Group>> GetGroups();
    IObservable<IEnumerable<Room>> GetGroupRooms(string groupId);
    IObservable<Room> CreateRoom(string groupId, CreateRoomRequest request);
    IObservable<IEnumerable<Room>> GetSuggestedRoomsFromGroup(string groupId);

    #endregion

    #region Search

    IObservable<SearchResponse<Room>> SearchRooms(string query, int limit = 10, int skip = 0);
    IObservable<SearchResponse<GitterUser>> SearchUsers(string query, int limit = 10, int skip = 0);
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

```c#
gitterApiService.GetCurrentUser()
				.Subscribe(user =>
					{
						// Execute code
					});
```

## [Streaming API](https://developer.gitter.im/docs/streaming-api)

The streaming API allows you to get realtime information.
Be careful, this method is not working sometimes. You should use the Realtime service if you want a more stable service.

### Realtime messages

Retrieve realtime new messages inside a room.

```c#
gitterApiService.GetRealtimeMessages(Room.Id)
                .Subscribe(message => 
					{
						// Do everything you need
						// This code will be executed each time the room receive a new message
					});
```

### Realtime events

Retrieve realtime new events inside a room.

```c#
gitterApiService.GetRealtimeEvents(Room.Id)
                .Subscribe(event => 
					{
						// Do everything you need
						// This code will be executed each time the room receive a new event
					});
```