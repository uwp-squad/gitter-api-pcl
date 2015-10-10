# gitter-api-pcl [![Join the chat at https://gitter.im/Odonno/gitter-api-pcl](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/Odonno/gitter-api-pcl?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

[![Build status](https://ci.appveyor.com/api/projects/status/dnoqp3gt2f6d6w2t?svg=true)](https://ci.appveyor.com/project/Odonno/gitter-api-pcl)

Gitter# provide you the minimum code you need to use the Gitter API.

## Authentication

The Gitter API requires you to retrieve a token to have access to the entire API. You can follow [this tutorial to get a token using OAuth2 authentication](https://developer.gitter.im/docs/authentication).

Now, you should receive a token. All you need to do is to provide it to the ApiService :

```
IGitterApiService gitterApiService = new GitterApiService();
gitterApiService.TryAuthenticate("the-token");
```

## User

### Current User

```
var currentUser = await gitterApiService.GetCurrentUserAsync();
```

## Unread items

### Unread messages

```
var unreadItems = await gitterApiService.RetrieveUnreadChatMessagesAsync("user-id", "room-id");
```

### Mark unread messages

```
IEnumerable<string> ids = new [] { "message-id", "another-message-id" };
await gitterApiService.MarkUnreadChatMessagesAsync("user-id", "room-id", ids);
```

## Room

### Retrieve rooms

```
var rooms = await gitterApiService.GetRoomsAsync();
```

### Join room

```
var room = await gitterApiService.JoinRoomAsync("room-uri");
```

## Messages

### Single message

```
var message = await gitterApiService.GetSingleRoomMessageAsync("room-id", "message-id");
```

### All room messages

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

### Send message

```
var message = await gitterApiService.SendMessageAsync("room-id", "this is a test message");
```

### Update message

```
var message = await gitterApiService.UpdateMessageAsync("room-id", "message-id", "this is an updated message");
```

## Realtime messages

To complete...