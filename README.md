# gitter-api-pcl [![Join the chat at https://gitter.im/Odonno/gitter-api-pcl](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/Odonno/gitter-api-pcl?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

[![NuGet Status](http://img.shields.io/nuget/v/gitter-api-pcl.svg?style=flat)](https://www.nuget.org/packages/gitter-api-pcl/)
[![Build status](https://ci.appveyor.com/api/projects/status/dnoqp3gt2f6d6w2t?svg=true)](https://ci.appveyor.com/project/Odonno/gitter-api-pcl)

*Gitter#* provide you the minimum code you need to use the Gitter API. Using .NET Standard 1.1, you can use *Gitter#* on these platforms :

* Windows 8.1 (Store Apps)
* Windows Phone 8.1
* Windows 10
* .NET Framework 4.5
* Xamarin.Android
* Xamarin.iOS
* .NET Core 1.0

The documentation is available in separate files dedicated to the possibilities of the package :

* [Authentication](/docs/auth.md), explains how to authenticate to the Gitter API and retrieve a token using [gitter-api-auth](https://github.com/Odonno/gitter-api-auth) package
* [Usual API](/docs/usual.md), the default API that returns async Tasks
* [Reactive API](/docs/reactive.md), a reactive version of the default API
* [Webhook](/docs/webhook.md), a way to use Webhook through Gitter
* [Upload images](/docs/upload-images.md), explains how to upload images