# Upload images as a message

If you want to upload images, you have to host the image by yourself and then use the library to send a message that contains the link to this new image.

## Upload image

If you want to upload, you can use any service that suits your needs. Here is a list of possible hosting services : 

* [imgur](https://imgur.com/)
* [transloadit](https://transloadit.com/)

Once you downloaded an image using one of these services, you can send a new message with the response link.

## Send the message

```
string fileName = "myImage.png";
string filePath = $"http://localhost/{fileName}";
string textMessage = $"[![{fileName}]({filePath})]({filePath})";

var message = await gitterApiService.SendMessageAsync("room-id", textMessage);
```