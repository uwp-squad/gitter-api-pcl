# Realtime

The Gitter API provide a true reliable realtime experience using Faye. 

```c#
public interface IRealtimeGitterService
{
    void Connect();
    void Disconnect();

    IObservable<RealtimeUserPresence> SubscribeToUserPresence(string roomId);
    IObservable<RealtimeChatMessage> SubscribeToChatMessages(string roomId);
    IObservable<RealtimeRoomUser> SubscribeToRoomUsers(string roomId);
    IObservable<RealtimeRoomEvent> SubscribeToRoomEvents(string roomId);
    IObservable<RealtimeReadBy> SubscribeToChatMessagesReadBy(string roomId, string messageId);
}
```

## Example

```c#
realtimeGitterService.SubscribeToChatMessages(Room.Id)
                .Subscribe(response => 
					{
                        if (response.Operation == "create")
                        {
                            // A new message has been created
                        }
                        if (response.Operation == "update")
                        {
                            // A new message has been updated
                        }
                        if (response.Operation == "remove")
                        {
                            // A new message has been removed
                        }
					});
```