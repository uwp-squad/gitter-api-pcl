# Webhook service

The webhook service provided by Gitter allows you to send messages in the activity panel of a room.

This webhook service give you a way to send two kinds of message :

* info messages, by default
* error messages (that is shown in red in activity panel)

*Also, there is no need of token for this part. The only thing requested is the Webhook Url possessed by the owner of the room.*

## The usual way

```c#
public interface IWebhookService
{
    Task<bool> PostAsync(string url, string message, MessageLevel level = MessageLevel.Info);
}
```

This way, you can send messages whenever you want like that :

```c#
var webhookService = new WebhookService();

bool infoPosted = await webhookService.PostAsync("webhook-url", "a simple message", MessageLevel.Info);

bool errorPosted = await webhookService.PostAsync("webhook-url", "an error message", MessageLevel.Error);
```

## The reactive way

```c#
public interface IReactiveWebhookService
{
    IObservable<bool> Post(string url, string message, MessageLevel level = MessageLevel.Info);
}
```

In a reactive way, you can send messages like that :

```c#
var webhookService = new ReactiveWebhookService();

webhookService.Post("webhook-url", "a simple message", MessageLevel.Info)
				.Subscribe(posted => {  });

webhookService.Post("webhook-url", "an error message", MessageLevel.Error)
				.Subscribe(posted => {  });
```