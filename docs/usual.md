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

    #endregion

    #region Unread Items

    Task<UnreadItems> RetrieveUnreadChatMessagesAsync(string userId, string roomId);
    Task MarkUnreadChatMessagesAsync(string userId, string roomId, IEnumerable<string> messageIds);

    #endregion

    #region Rooms

    Task<IEnumerable<Room>> GetRoomsAsync();
	Task<IEnumerable<User>> GetRoomUsersAsync(string roomId, int limit = 30, string q = null, int skip = 0);
    Task<Room> JoinRoomAsync(string roomName);

    #endregion

    #region Messages

    Task<Message> GetSingleRoomMessageAsync(string roomId, string messageId);
    Task<IEnumerable<Message>> GetRoomMessagesAsync(string roomId, int limit = 50, string beforeId = null, string afterId = null, int skip = 0);
    Task<Message> SendMessageAsync(string roomId, string message);
    Task<Message> UpdateMessageAsync(string roomId, string messageId, string message);

    #endregion

    #region Events

    Task<IEnumerable<RoomEvent>> GetRoomEventsAsync(string roomId);

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
    public string Uri { get; set; }
    public bool IsPrivate { get; set; }
    public Room Room { get; set; }
}
```

### [Current User](https://developer.gitter.im/docs/user-resource#get-the-current-user)

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
    public string Url { get; set; }
    public bool OneToOne { get; set; }
    public User User { get; set; }
    public IList<User> Users { get; set; }
    public int UserCount { get; set; }
    public int UnreadItems { get; set; }
    public int UnreadMentions { get; set; }
    public DateTime LastAccessTime { get; set; }
    public bool Favourite { get; set; }
    public bool DisabledNotifications { get; set; }
    public string Type { get; set; }
    public IEnumerable<string> Tags { get; set; }
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

The current user join a room. The parameter *room-name* looks like this : *Odonno/Modern-Gitter*.

```
var room = await gitterApiService.JoinRoomAsync("room-name");
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

Retrieve multiple messages contained in a room. There is multiple parameters you can use to define a more precise request.

```
var messages = await gitterApiService.GetRoomMessagesAsync("room-id");
```

By default, the *limit* of messages you can get using this request is 50.

```
var messages = await gitterApiService.GetRoomMessagesAsync("room-id", 20);
```

Of course, you can overload this parameter like this. Here, we set the *limit* to 20.

```
var messages = await gitterApiService.GetRoomMessagesAsync("room-id", 20, "before-message-id");
```

You can also add a parameter which expect a message id (*beforeId*). Using this parameter, you will only receive the messages before the one you set in parameter.

```
var messages = await gitterApiService.GetRoomMessagesAsync("room-id", 20, "before-message-id", "after-message-id");
```

There is also another parameter to set the *afterId*. It is the exact opposite effect of *beforeId* parameter.

```
var messages = await gitterApiService.GetRoomMessagesAsync("room-id", 20, "before-message-id", "after-message-id", 10);
```

Then, you have a way to *skip* some messages. Here, with the previous request, we ask to retrieve 20 messages before the 10 we skip.

```
var messages = await gitterApiService.GetRoomMessagesAsync("room-id", 20, null, null, 10);
```

Of course, you can also remove some parameters if you need the *skip* parameter but neither *beforeId* nor *afterId*.


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