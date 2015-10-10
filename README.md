# gitter-api-pcl [![Join the chat at https://gitter.im/Odonno/gitter-api-pcl](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/Odonno/gitter-api-pcl?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

[![Build status](https://ci.appveyor.com/api/projects/status/dnoqp3gt2f6d6w2t?svg=true)](https://ci.appveyor.com/project/Odonno/gitter-api-pcl)

Gitter# provide you the minimum code you need to use the Gitter API. You can use Gitter# on these platforms :

* Windows 8.1 (Store Apps)
* Windows Phone 8.1
* Windows 10
* .NET Framework 4.5 [planned]
* Xamarin.Android [planned]
* Xamarin.iOS [planned]

## Authentication

The Gitter API requires you to retrieve a token to have access to the entire API. You can follow [this tutorial to get a token using OAuth2 authentication](https://developer.gitter.im/docs/authentication).

Now, you should receive a token. All you need to do is to provide it to the ApiService :

```
IGitterApiService gitterApiService = new GitterApiService();
gitterApiService.TryAuthenticate("the-token");
```

*Be careful, you need to set the token using TryAuthenticate method each time you create a new instance of GitterApiService.*

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

### [Current User](https://developer.gitter.im/docs/user-resource#get-the-current-user)

Retrieve information about the user logged in (after authentication).

```
var currentUser = await gitterApiService.GetCurrentUserAsync();
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

## Room

### [Retrieve rooms](https://developer.gitter.im/docs/rooms-resource#list-rooms)

```
var rooms = await gitterApiService.GetRoomsAsync();
```

### [Join room](https://developer.gitter.im/docs/rooms-resource#join-a-room)

```
var room = await gitterApiService.JoinRoomAsync("room-uri");
```

## Messages

### [Single message](https://developer.gitter.im/docs/messages-resource#single-message)

```
var message = await gitterApiService.GetSingleRoomMessageAsync("room-id", "message-id");
```

### [All room messages](https://developer.gitter.im/docs/messages-resource#list-messages)

```
var messages = await gitterApiService.GetRoomMessagesAsync("room-id");
```

```
var messages = await gitterApiService.GetRoomMessagesAsync("room-id", 20);
```

```
var messages = await gitterApiService.GetRoomMessagesAsync("room-id", 20, "message-id");
```

```
var messages = await gitterApiService.GetRoomMessagesAsync("room-id", 20, "message-id", "another-message-id");
```

```
var messages = await gitterApiService.GetRoomMessagesAsync("room-id", 20, "message-id", "another-message-id", 10);
```

### [Send message](https://developer.gitter.im/docs/messages-resource#send-a-message)

```
var message = await gitterApiService.SendMessageAsync("room-id", "this is a test message");
```

### [Update message](https://developer.gitter.im/docs/messages-resource#update-a-message)

```
var message = await gitterApiService.UpdateMessageAsync("room-id", "message-id", "this is an updated message");
```

## [Streaming API](https://developer.gitter.im/docs/streaming-api)

### Realtime messages

To complete...

### Realtime events

Not available...