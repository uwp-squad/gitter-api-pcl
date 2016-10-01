# Gitter API as a service

With Gitter#, you can use the Gitter API as a service. So, everything you need (almost) is inside a single place : *GitterApiService*.

```
public interface IGitterApiService
{
    #region Properties

    string Token { get; set; }

    #endregion

    #region User

    Task<User> GetCurrentUserAsync();
    Task<IEnumerable<Organization>> GetOrganizationsAsync(string userId);
    Task<IEnumerable<Repository>> GetRepositoriesAsync(string userId);
    Task<IEnumerable<Room>> GetSuggestedRoomsAsync();

    #endregion

    #region Unread Items

    Task<UnreadItems> RetrieveUnreadChatMessagesAsync(string userId, string roomId);
    Task MarkUnreadChatMessagesAsync(string userId, string roomId, IEnumerable<string> messageIds);

    #endregion

    #region Rooms

    Task<IEnumerable<Room>> GetRoomsAsync();
	Task<IEnumerable<User>> GetRoomUsersAsync(string roomId, int limit = 30, string q = null, int skip = 0);
    Task<Room> JoinRoomAsync(string roomName);
    Task<Room> JoinRoomAsync(string userId, string roomId);
    Task<Room> UpdateRoomAsync(string roomId, UpdateRoomRequest request);
    Task<bool> UpdateUserRoomSettingsAsync(string userId, string roomId, UpdateUserRoomSettingsRequest request);
    Task<SuccessResponse> LeaveRoomAsync(string roomId, string userId);
    Task<SuccessResponse> DeleteRoomAsync(string roomId);
    Task<IEnumerable<Room>> GetSuggestedRoomsAsync(string roomId);
    Task<IEnumerable<Collaborator>> GetSuggestedCollaboratorsOnRoomAsync(string roomId);
    Task<IEnumerable<RoomIssue>> GetRoomIssuesAsync(string roomId);
    Task<IEnumerable<Ban>> GetRoomBansAsync(string roomId);
    Task<WelcomeMessage> GetWelcomeMessageAsync(string roomId);
    Task<UpdateWelcomeMessageResponse> UpdateWelcomeMessageAsync(string roomId, UpdateWelcomeMessageRequest request);

    #endregion

    #region Messages

    Task<Message> GetSingleRoomMessageAsync(string roomId, string messageId);
    Task<IEnumerable<Message>> GetRoomMessagesAsync(string roomId, MessageRequest request);
    Task<Message> SendMessageAsync(string roomId, string message);
    Task<Message> UpdateMessageAsync(string roomId, string messageId, string message);

    #endregion

    #region Events

    Task<IEnumerable<RoomEvent>> GetRoomEventsAsync(string roomId);

    #endregion

    #region Groups

    Task<IEnumerable<Group>> GetGroupsAsync();
    Task<IEnumerable<Room>> GetGroupRoomsAsync(string groupId);
    Task<Room> CreateRoomAsync(string groupId, CreateRoomRequest request);

    #endregion

    #region Search

    Task<SearchResponse<Room>> SearchRoomsAsync(string query, int limit = 10);
    Task<SearchResponse<User>> SearchUsersAsync(string query, int limit = 10);
    Task<SearchResponse<Repository>> SearchUserRepositoriesAsync(string userId, string query, int limit = 10);

    #endregion

    #region Analytics

    Task<Dictionary<DateTime, int>> GetRoomMessagesCountByDayAsync(string roomId);

    #endregion
}
```

**Be careful, as no usual methods allow us to send incoming streaming messages, the streaming API could only be available using the [reactive](/docs/reactive.md) version of this service.**

## User

```
public class User
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public string Url { get; set; }
    public string SmallAvatarUrl { get; set; }
    public string MediumAvatarUrl { get; set; }
    public IEnumerable<string> Providers { get; set; }
    public bool Staff { get; set; }
    public int Version { get; set; }
    public string GravatarVersion { get; set; }
    public string GitHubUrl { get; }
}
```

```
public class Organization
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Avatar { get; set; }
    public Room Room { get; set; }
}
```

```
public class Repository
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Uri { get; set; }
    public bool IsPrivate { get; set; }
    public Room Room { get; set; }
    public bool Exists { get; set; }
    public string AvatarUrl { get; set; }
}
```

### [Current User](https://developer.gitter.im/docs/authentication#check-who-you-are-authenticated-as)

Retrieve information about the user logged in (after authentication).

```
var currentUser = await gitterApiService.GetCurrentUserAsync();
```

### [Organizations](https://developer.gitter.im/docs/user-resource#orgs)

Retrieve the list of organizations of the current user logged.

```
var currentUser = await gitterApiService.GetCurrentUserAsync();
var organizations = await gitterApiService.GetOrganizationsAsync(currentUser.Id);
```

### [Repositories](https://developer.gitter.im/docs/user-resource#repos)

Retrieve the list of repositories of the current user logged.

```
var currentUser = await gitterApiService.GetCurrentUserAsync();
var repositories = await gitterApiService.GetRepositoriesAsync(currentUser.Id);
```

### Suggested rooms

Retrieve suggested rooms for the current user

```
var rooms = await gitterApiService.GetSuggestedRoomsAsync();
```

## Unread items

```
public class UnreadItems
{
    public IEnumerable<string> Messages { get; set; }
    public IEnumerable<string> Mentions { get; set; }
}
```

### [Unread items](https://developer.gitter.im/docs/user-resource#unread-items)

Retrieve unread items (messages + mentions) of a specific room. Each list of string contains a list of message id.

```
var unreadItems = await gitterApiService.RetrieveUnreadChatMessagesAsync("user-id", "room-id");
```

### [Mark unread messages](https://developer.gitter.im/docs/user-resource#mark-unread-items)

Mark unread messages for the user who are currently reading the messages. You need to pass the list of message id.

```
IEnumerable<string> ids = new [] { "message-id", "another-message-id" };
await gitterApiService.MarkUnreadChatMessagesAsync("user-id", "room-id", ids);
```

## Rooms

```
public class Room
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Topic { get; set; }
    public string AvatarUrl { get; set; }
    public string Uri { get; set; }
    public bool OneToOne { get; set; }
    public int UserCount { get; set; }
    public User User { get; set; }
    public int UnreadItems { get; set; }
    public int UnreadMentions { get; set; }
    public DateTime LastAccessTime { get; set; }
    public bool Favourite { get; set; }
    public bool DisabledNotifications { get; set; }
    public string Url { get; set; }
    public string Type { get; set; }
    public string Security { get; set; }
    public bool Premium { get; set; }
    public bool NoIndex { get; set; }
    public IEnumerable<string> Tags { get; set; }
    public bool RoomMember { get; set; }
    public string GroupId { get; set; }
    public bool Public { get; set; }
    public int Version { get; set; }
    public string GitHubUrl { get; }
    public string Image { get; }
}
```

### [Retrieve rooms](https://developer.gitter.im/docs/rooms-resource#list-rooms)

Retrieve all rooms where the current user is in.

```
var rooms = await gitterApiService.GetRoomsAsync();
```

### [Retrieve room users](https://developer.gitter.im/docs/rooms-resource#users)

Returns list of users in the room.

```
var users = await gitterApiService.GetRoomUsersAsync("room-id");
```

### [Join room](https://developer.gitter.im/docs/rooms-resource#join-a-room)

THere is two endpoints to join a room, by room name or by room id.

#### Join room by name

The current user join a room. The parameter *room-name* looks like this : *Odonno/Modern-Gitter*.

```
var room = await gitterApiService.JoinRoomAsync("room-name");
```

#### Join room by id

```
var room = await gitterApiService.JoinRoomAsync("user-id", "room-id");
```

### Update room

```
public class UpdateRoomRequest
{
    public string Topic { get; set; }
    public bool NoIndex { get; set; }
    public string Tags { get; set; }
}
```

Update some room information.

```
var request = new UpdateRoomRequest
{
    Topic = "A gitter API library for .NET applications",
    Tags = "gitter, api, csharp"
};
var room = await gitterApiService.UpdateRoomAsync("room-id", request);
```

Attention ! Notice that `tags` property is not returned by the response... 

### Update user room settings

```
public class UpdateUserRoomSettingsRequest
{
    public bool Favourite { get; set; }
}
```

Update settings of the user on a specific room. Example below shows that user X set room Y as favourite.

```
var request = new UpdateUserRoomSettingsRequest
{
    Favourite = true
};
bool success = await gitterApiService.UpdateUserRoomSettingsAsync("user-id", "room-id", request);
```

### Leave room

Leave room (if it is current user) or remove user from room if we have rights to do it.

```
var successResponse = await gitterApiService.LeaveRoomAsync("room-id", "user-id");
```

### Delete room

Delete room by id.

```
var successResponse = await gitterApiService.DeleteRoomAsync("room-id");
```

### Suggested rooms

Retrieve all suggested rooms based on a room.

```
var rooms = await gitterApiService.GetSuggestedRoomsAsync("room-id");
```

### Suggested collaborators

Retrieve collaborators that you can invite to join the room.

```
public class Collaborator
{
    public string DisplayName { get; set; }
    public string ExternalId { get; set; }
    public string AvatarUrl { get; set; }
    public string Type { get; set; }
}
```

```
var collaborators = await gitterApiService.GetSuggestedCollaboratorsOnRoomAsync("room-id");
```

### Room Issues

Retrieve issues on a room.

```
public class RoomIssue
{
    public string Title { get; set; }
    public string Number { get; set; }
}
```

```
var issues = await gitterApiService.GetRoomIssuesAsync("room-id");
```

### Room banned users

Retrieve banned users on a room.

```
public class Ban
{
    public User User { get; set; }
    public User BannedBy { get; set; }
    public DateTime Date { get; set; }
}
```

```
var bans = await gitterApiService.GetRoomBansAsync("room-id");
```

### Welcome message

Retrieve welcome message of the room

```
var welcomeMessage = await gitterApiService.GetWelcomeMessageAsync("room-id");
```

### Update welcome message

Update welcome message of the room

```
var request = new UpdateWelcomeMessageRequest
{
    Content = "A welcome message"
};
var result = await gitterApiService.UpdateWelcomeMessageAsync("room-id", request);
```

## Messages

```
public class Message
{
    public string Id { get; set; }
    public string Text { get; set; }
    public string Html { get; set; }
    public DateTime SentDate { get; set; }
    public DateTime? EditedDate { get; set; }
    public User User { get; set; }
    public bool UnreadByCurrent { get; set; }
    public int ReadCount { get; set; }
    public IEnumerable<MessageUrl> Urls { get; set; }
    public IEnumerable<Mention> Mentions { get; set; }
    public IEnumerable<Issue> Issues { get; set; }
    public int Version { get; set; }
}
```

```
public class MessageUrl
{
    public string Url { get; set; }
}
```

```
public class Mention
{
    public string ScreenName { get; set; }
    public string UserId { get; set; }
}
```

```
public class Issue
{
    public string Number { get; set; }
}
```

### [Single message](https://developer.gitter.im/docs/messages-resource#get-a-single-message)

Retrieve a single message based on its id.

```
var message = await gitterApiService.GetSingleRoomMessageAsync("room-id", "message-id");
```

### [All room messages](https://developer.gitter.im/docs/messages-resource#list-messages)

```
public class MessageRequest
{
    public int Limit { get; set; } = 50;
    public string BeforeId { get; set; }
    public string AfterId { get; set; }
    public string AroundId { get; set; }
    public int Skip { get; set; }
    public string Query { get; set; }
    public string Lang { get; set; }
}
```

Retrieve multiple messages contained in a room. There is multiple parameters you can use to define a more precise request.

```
var request = new MessageRequest();
var messages = await gitterApiService.GetRoomMessagesAsync("room-id", request);
```

By default, the *limit* of messages you can get using this request is 50.

```
var request = new MessageRequest
{
    Limit = 20
};
var messages = await gitterApiService.GetRoomMessagesAsync("room-id", request);
```

Of course, you can overload this parameter like this. Here, we set the *limit* to 20.

```
var request = new MessageRequest
{
    Limit = 20,
    BeforeId = "before-message-id",
    AfterId = "after-message-id",
    Skip = 10,
    Query = "js"
};
var messages = await gitterApiService.GetRoomMessagesAsync("room-id", request);
```

You can also add a parameter which expect a message id (*beforeId*). Using this parameter, you will only receive the messages before the one you set in parameter.
There is also another parameter to set the *afterId*. It is the exact opposite effect of *beforeId* parameter.
Then, you have a way to *skip* some messages. Here, with the previous request, we ask to retrieve 20 messages before the 10 we skip.
You can also search specific topic inside messages with *q* (query) parameter.

### [Send message](https://developer.gitter.im/docs/messages-resource#send-a-message)

Send a new message to the room.

```
var message = await gitterApiService.SendMessageAsync("room-id", "this is a test message");
```

### [Update message](https://developer.gitter.im/docs/messages-resource#update-a-message)

Update an existing message.

```
var message = await gitterApiService.UpdateMessageAsync("room-id", "message-id", "this is an updated message");
```

## Events

```
public class RoomEvent
{
    public string Id { get; set; }
    public string Text { get; set; }
    public string Html { get; set; }
    public DateTime SentDate { get; set; }
    public DateTime? EditedDate { get; set; }
    public Meta Meta { get; set; }
    public int Version { get; set; }
}
```

### List of room events

Retrieve all room events.

```
var events = await gitterApiService.GetRoomEventsAsync("room-id");
```

## Groups

```
public class Group
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Uri { get; set; }
    public GroupSecurity BackedBy { get; set; }
    public string AvatarUrl { get; set; }
}
```

### [List of groups](https://developer.gitter.im/docs/groups-resource#list-groups)

```
var groups = await gitterApiService.GetGroupsAsync("room-id");
```

### [List of rooms in a group](https://developer.gitter.im/docs/groups-resource#list-rooms-under-group)

```
var rooms = await gitterApiService.GetGroupRoomsAsync("group-id");
```

### Create room

The user can create a room, generally a channel, based on a name and other parameters.

```
public class CreateRoomRequest
{
    public string Name { get; set; }
    public string Topic { get; set; }
    public bool AddBadge { get; set; }
}
```

```
var request = new CreateRoomRequest { Name = "test" };
var room = await gitterApiService.CreateRoomAsync("group-id", request);
```

## Search

### Search rooms

Search rooms by name.

```
var results = await gitterApiService.SearchRoomsAsync("test", 50);
```

### Search users

Search users by name.

```
var results = await gitterApiService.SearchUsersAsync("test", 50);
```

### Search user repositories

Search repositories of a user.

```
var results = await gitterApiService.SearchUserRepositoriesAsync("user-id", "test", 50);
```

## Analytics

### Get room messages count by day

This method is mainly use to build a heatmap of room messages.

```
var datesWithCount = await gitterApiService.GetRoomMessagesCountByDayAsync("room-id");
```