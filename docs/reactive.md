# Gitter API is also reactive 

## [Streaming API](https://developer.gitter.im/docs/streaming-api)

### Realtime messages

Retrieve realtime new messages in inside a room.

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