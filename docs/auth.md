# Authentication

The Gitter API requires you to retrieve a token to have access to the entire API. You can follow [this tutorial to get a token using OAuth2 authentication](https://developer.gitter.im/docs/authentication).

Now, you should receive a token. All you need to do is to provide it to the ApiService :

```
IGitterApiService gitterApiService = new GitterApiService();
gitterApiService.Token = "the-token";
```

In case, you want to set instantly by creating a new instance of *GitterApiService*, you can use a specific constructor :

```
IGitterApiService gitterApiService = new GitterApiService("the-token");
```

*Be careful, you need to set the token each time you create a new instance of GitterApiService.*

**Moreover, I've created another project / NuGet package that you can use to do authentication easily. You can retrieve this package following the GitHub repository here : [https://github.com/Odonno/gitter-api-auth](https://github.com/Odonno/gitter-api-auth).**