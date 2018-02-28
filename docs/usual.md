# Gitter API as a service

With Gitter#, you can use the Gitter API as a service. So, everything you need (almost) is inside a single place : *GitterApiService*.

```c#
public interface IGitterApiService
{
    #region Properties

    string Token { get; set; }

    #endregion

    #region Repository

    Task<RepositoryInfo> GetRepositoryInfoAsync(string repositoryName);

    #endregion

    #region User

    Task<GitterUser> GetCurrentUserAsync();
    Task<IEnumerable<Organization>> GetMyOrganizationsAsync(bool unused = false);
    Task<IEnumerable<Organization>> GetOrganizationsAsync(string userId);
    Task<IEnumerable<Repository>> GetMyRepositoriesAsync(string query, int limit = 0);
    Task<IEnumerable<Repository>> GetMyRepositoriesAsync(bool unused = false);
    Task<IEnumerable<Repository>> GetRepositoriesAsync(string userId);
    Task<IEnumerable<Room>> GetSuggestedRoomsAsync();
    Task<IEnumerable<RoomUnreadCount>> GetAggregatedUnreadItemsAsync();
    Task<UserInfo> GetUserInfoAsync(string username);

    #endregion

    #region Unread Items

    Task<UnreadItems> RetrieveUnreadChatMessagesAsync(string userId, string roomId);
    Task MarkUnreadChatMessagesAsync(string userId, string roomId, IEnumerable<string> messageIds);

    #endregion

    #region Rooms

    Task<IEnumerable<Room>> GetRoomsAsync();
	Task<IEnumerable<User>> GetRoomUsersAsync(string roomId, int limit = 30, string q = null, int skip = 0);
    Task<Room> GetRoomAsync(string roomName);
    Task<Room> JoinRoomAsync(string userId, string roomId);
    Task<Room> UpdateRoomAsync(string roomId, UpdateRoomRequest request);
    Task<bool> UpdateUserRoomSettingsAsync(string userId, string roomId, UpdateUserRoomSettingsRequest request);
    Task<RoomNotificationSettingsResponse> GetRoomNotificationSettingsAsync(string userId, string roomId);
    Task<RoomNotificationSettingsResponse> UpdateRoomNotificationSettingsAsync(string userId, string roomId, UpdateRoomNotificationSettingsRequest request);
    Task<SuccessResponse> LeaveRoomAsync(string roomId, string userId);
    Task<SuccessResponse> DeleteRoomAsync(string roomId);
    Task<IEnumerable<Room>> GetSuggestedRoomsAsync(string roomId);
    Task<IEnumerable<Collaborator>> GetSuggestedCollaboratorsOnRoomAsync(string roomId);
    Task<IEnumerable<RoomIssue>> GetRoomIssuesAsync(string roomId);
    Task<IEnumerable<Ban>> GetRoomBansAsync(string roomId);
    Task<BanUserResponse> BanUserFromRoomAsync(string roomId, string username, bool removeMessages = false);
    Task<SuccessResponse> UnbanUserAsync(string roomId, string userId);
    Task<WelcomeMessage> GetWelcomeMessageAsync(string roomId);
    Task<UpdateWelcomeMessageResponse> UpdateWelcomeMessageAsync(string roomId, UpdateWelcomeMessageRequest request);
    Task<InviteUserResponse> InviteUserInRoomAsync(string roomId, InviteUserRequest request); 

    #endregion

    #region Messages

    Task<Message> GetSingleRoomMessageAsync(string roomId, string messageId);
    Task<IEnumerable<Message>> GetRoomMessagesAsync(string roomId, MessageRequest request);
    Task<Message> SendMessageAsync(string roomId, string message);
    Task<Message> UpdateMessageAsync(string roomId, string messageId, string message);
    Task DeleteMessageAsync(string roomId, string messageId);
    Task<IEnumerable<GitterUser>> GetUsersWhoReadMessageAsync(string roomId, string messageId);

    #endregion

    #region Events

    Task<IEnumerable<RoomEvent>> GetRoomEventsAsync(string roomId, int limit = 50, int skip = 0, string beforeId = null);

    #endregion

    #region Groups

    Task<IEnumerable<Group>> GetGroupsAsync();
    Task<IEnumerable<Room>> GetGroupRoomsAsync(string groupId);
    Task<Room> CreateRoomAsync(string groupId, CreateRoomRequest request);
    Task<IEnumerable<Room>> GetSuggestedRoomsFromGroupAsync(string groupId);

    #endregion

    #region Search

    Task<SearchResponse<Room>> SearchRoomsAsync(string query, int limit = 10, int skip = 0);
    Task<SearchResponse<GitterUser>> SearchUsersAsync(string query, int limit = 10, int skip = 0);
    Task<SearchResponse<Repository>> SearchUserRepositoriesAsync(string userId, string query, int limit = 10);

    #endregion

    #region Analytics

    Task<Dictionary<DateTime, int>> GetRoomMessagesCountByDayAsync(string roomId);

    #endregion
}
```

**Be careful, as no usual methods allow us to send incoming streaming messages, the streaming API could only be available using the [reactive](/docs/reactive.md) version of this service.**

## Repository

```c#
public class GitHubUser
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string AvatarUrl { get; set; }
    public string GravatarId { get; set; }
    public string ApiUrl { get; set; }
    public string WebsiteUrl { get; set; }
    public string FollowersApiUrl { get; set; }
    public string FollowingApiUrl { get; set; }
    public string GistsApiUrl { get; set; }
    public string StarredApiUrl { get; set; }
    public string SubscriptionsApiUrl { get; set; }
    public string OrgsApiUrl { get; set; }
    public string ReposApiUrl { get; set; }
    public string EventsApiUrl { get; set; }
    public string ReceivedEventsApiUrl { get; set; }
    public string Type { get; set; }
    public bool IsGitHubAdmin { get; set; }
}
```

```c#
public class GitHubPermission
{
    public bool Admin { get; set; }
    public bool Push { get; set; }
    public bool Pull { get; set; }
}
```

```c#
public class RepositoryInfo
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public GitHubUser Owner { get; set; }
    public bool IsPrivate { get; set; }
    public string WebsiteUrl{ get; set; }
    public string Description { get; set; }
    public bool IsFork { get; set; }
    public string ApiUrl { get; set; }
    public string ForksApiUrl { get; set; }
    public string KeysApiUrl { get; set; }
    public string CollaboratorsApiUrl { get; set; }
    public string TeamsApiUrl { get; set; }
    public string HooksApiUrl { get; set; }
    public string IssueEventsApiUrl { get; set; }
    public string EventsApiUrl { get; set; }
    public string AssigneesApiUrl { get; set; }
    public string BranchesApiUrl { get; set; }
    public string TagsApiUrl { get; set; }
    public string BlobsApiUrl { get; set; }
    public string GitTagsApiUrl { get; set; }
    public string GitRefsApiUrl { get; set; }
    public string TreesApiUrl { get; set; }
    public string StatusesApiUrl { get; set; }
    public string LanguagesApiUrl { get; set; }
    public string StargazersApiUrl { get; set; }
    public string ContributorsApiUrl { get; set; }
    public string SubscribersApiUrl { get; set; }
    public string SubscriptionApiUrl { get; set; }
    public string CommitsApiUrl { get; set; }
    public string GitCommitsApiUrl { get; set; }
    public string CommentsApiUrl { get; set; }
    public string IssueCommentsApiUrl { get; set; }
    public string ContentsApiUrl { get; set; }
    public string CompareApiUrl { get; set; }
    public string MergesApiUrl { get; set; }
    public string ArchiveApiUrl { get; set; }
    public string DownloadsApiUrl { get; set; }
    public string IssuesApiUrl { get; set; }
    public string PullsApiUrl { get; set; }
    public string MilestonesApiUrl { get; set; }
    public string NotificationsApiUrl { get; set; }
    public string LabelsApiUrl { get; set; }
    public string ReleasesApiUrl { get; set; }
    public string DeploymentsApiUrl { get; set; }
    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
    public string PushedAt { get; set; }
    public string GitUrl { get; set; }
    public string SshUrl { get; set; }
    public string CloneUrl { get; set; }
    public string SvnUrl { get; set; }
    public string HomepageUrl { get; set; }
    public long Size { get; set; }
    public int StargazersCount { get; set; }
    public int WatchersCount { get; set; }
    public string Language { get; set; }
    public bool HasIssues { get; set; }
    public bool HasProjects { get; set; }
    public bool HasDownloads { get; set; }
    public bool HasWiki { get; set; }
    public bool HasPages { get; set; }
    public int ForksCount { get; set; }
    public string MirrorUrl { get; set; }
    public int OpenIssuesCount { get; set; }
    public int Forks { get; set; }
    public int OpenIssues { get; set; }
    public int Watchers { get; set; }
    public string DefaultBranch { get; set; }
    public GitHubPermission Permissions { get; set; }
    public bool AllowSquashMerge { get; set; }
    public bool AllowMergeCommit { get; set; }
    public bool AllowRebaseMerge { get; set; }
    public GitHubUser Organization { get; set; }
    public int NetworkCount { get; set; }
    public int SubscribersCount { get; set; }
}
```

### Repository info

Retrieve repository information from GitHub API.

```c#
var repositoryInfo = await gitterApiService.GetRepositoryInfoAsync("owner/repoName");
```

## User

```c#
public class GitterUser
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

```c#
public class Organization
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Avatar { get; set; }
    public Room Room { get; set; }
}
```

```c#
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

```c#
var currentUser = await gitterApiService.GetCurrentUserAsync();
```

### [Organizations](https://developer.gitter.im/docs/user-resource#orgs)

Retrieve the list of organizations of the current user logged.

```c#
var currentUser = await gitterApiService.GetCurrentUserAsync();
var organizations = await gitterApiService.GetOrganizationsAsync(currentUser.Id);
```

### [Repositories](https://developer.gitter.im/docs/user-resource#repos)

Retrieve the list of repositories of the current user logged.

```c#
var currentUser = await gitterApiService.GetCurrentUserAsync();
var repositories = await gitterApiService.GetRepositoriesAsync(currentUser.Id);
```

### Suggested rooms

Retrieve suggested rooms for the current user

```c#
var rooms = await gitterApiService.GetSuggestedRoomsAsync();
```

## Unread items

```c#
public class UnreadItems
{
    public IEnumerable<string> Messages { get; set; }
    public IEnumerable<string> Mentions { get; set; }
}
```

### [Unread items](https://developer.gitter.im/docs/user-resource#unread-items)

Retrieve unread items (messages + mentions) of a specific room. Each list of string contains a list of message id.

```c#
var unreadItems = await gitterApiService.RetrieveUnreadChatMessagesAsync("user-id", "room-id");
```

### [Mark unread messages](https://developer.gitter.im/docs/user-resource#mark-unread-items)

Mark unread messages for the user who are currently reading the messages. You need to pass the list of message id.

```c#
IEnumerable<string> ids = new [] { "message-id", "another-message-id" };
await gitterApiService.MarkUnreadChatMessagesAsync("user-id", "room-id", ids);
```

## Rooms

```c#
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

```c#
var rooms = await gitterApiService.GetRoomsAsync();
```

### [Retrieve room users](https://developer.gitter.im/docs/rooms-resource#users)

Returns list of users in the room.

```c#
var users = await gitterApiService.GetRoomUsersAsync("room-id");
```

### [Join room](https://developer.gitter.im/docs/rooms-resource#join-a-room)

THere is two endpoints to join a room, 

* by room name (only for one-to-one room) 
* by room id

#### Get room information by name

The current user join a room. The parameter *room-name* looks like this : *Odonno/Modern-Gitter*.

```c#
var room = await gitterApiService.GetRoomAsync("room-name");
```

#### Join room by id

```c#
var room = await gitterApiService.JoinRoomAsync("user-id", "room-id");
```

### Update room

```c#
public class UpdateRoomRequest
{
    public string Topic { get; set; }
    public bool NoIndex { get; set; }
    public string Tags { get; set; }
}
```

Update some room information.

```c#
var request = new UpdateRoomRequest
{
    Topic = "A gitter API library for .NET applications",
    Tags = "gitter, api, csharp"
};
var room = await gitterApiService.UpdateRoomAsync("room-id", request);
```

Attention ! Notice that `tags` property is not returned by the response... 

### Update user room settings

```c#
public class UpdateUserRoomSettingsRequest
{
    public bool Favourite { get; set; }
}
```

Update settings of the user on a specific room. Example below shows that user X set room Y as favourite.

```c#
var request = new UpdateUserRoomSettingsRequest
{
    Favourite = true
};
bool success = await gitterApiService.UpdateUserRoomSettingsAsync("user-id", "room-id", request);
```

### Get room notification settings

```c#
public class RoomNotificationSettingsResponse
{
    public string Push { get; set; }
    public string Mode { get; set; }
    public bool Lurk { get; set; }
    public bool Unread { get; set; }
    public bool Activity { get; set; }
    public bool Mention { get; set; }
    public bool Announcement { get; set; }
    public bool Desktop { get; set; }
    public bool Mobile { get; set; }
    public bool Default { get; set; }
    public DefaultNotificationSettingsReponse DefaultSettings { get; set; }
}
```

Get notification settings on a specific room.

```c#
var response = await gitterApiService.GetRoomNotificationSettingsAsync("user-id", "room-id");
```

### Update room notification settings

```c#
public class UpdateRoomNotificationSettingsRequest
{
    public string Mode { get; set; }
}
```

Update notification settings on a specific room. Example below shows that user X set notification mode on room Y to "mute" mode.

```c#
var request = new UpdateRoomNotificationSettingsRequest
{
    Mode = NotificationMode.Mute
};
var response = await gitterApiService.UpdateRoomNotificationSettingsAsync("user-id", "room-id", request);
```

By default, room notification mode is set to `NotificationMode.All`.

### Leave room

Leave room (if it is current user) or remove user from room if we have rights to do it.

```c#
var successResponse = await gitterApiService.LeaveRoomAsync("room-id", "user-id");
```

### Delete room

Delete room by id.

```c#
var successResponse = await gitterApiService.DeleteRoomAsync("room-id");
```

### Suggested rooms

Retrieve all suggested rooms based on a room.

```c#
var rooms = await gitterApiService.GetSuggestedRoomsAsync("room-id");
```

### Suggested collaborators

Retrieve collaborators that you can invite to join the room.

```c#
public class Collaborator
{
    public string DisplayName { get; set; }
    public string ExternalId { get; set; }
    public string AvatarUrl { get; set; }
    public string Type { get; set; }
}
```

```c#
var collaborators = await gitterApiService.GetSuggestedCollaboratorsOnRoomAsync("room-id");
```

### Room Issues

Retrieve issues on a room.

```c#
public class RoomIssue
{
    public string Title { get; set; }
    public string Number { get; set; }
}
```

```c#
var issues = await gitterApiService.GetRoomIssuesAsync("room-id");
```

### Room banned users

Retrieve banned users on a room.

```c#
public class Ban
{
    public User User { get; set; }
    public User BannedBy { get; set; }
    public DateTime Date { get; set; }
}
```

```c#
var bans = await gitterApiService.GetRoomBansAsync("room-id");
```

### Ban user from room

Ban user from room.

```c#
var banResponse = await gitterApiService.BanUserFromRoomAsync("room-id", "username");
```

### Welcome message

Retrieve welcome message of the room

```c#
var welcomeMessage = await gitterApiService.GetWelcomeMessageAsync("room-id");
```

### Update welcome message

Update welcome message of the room

```c#
var request = new UpdateWelcomeMessageRequest
{
    Content = "A welcome message"
};
var result = await gitterApiService.UpdateWelcomeMessageAsync("room-id", request);
```

## Messages

```c#
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

```c#
public class MessageUrl
{
    public string Url { get; set; }
}
```

```c#
public class Mention
{
    public string ScreenName { get; set; }
    public string UserId { get; set; }
}
```

```c#
public class Issue
{
    public string Number { get; set; }
}
```

### [Single message](https://developer.gitter.im/docs/messages-resource#get-a-single-message)

Retrieve a single message based on its id.

```c#
var message = await gitterApiService.GetSingleRoomMessageAsync("room-id", "message-id");
```

### [All room messages](https://developer.gitter.im/docs/messages-resource#list-messages)

```c#
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

```c#
var request = new MessageRequest();
var messages = await gitterApiService.GetRoomMessagesAsync("room-id", request);
```

By default, the *limit* of messages you can get using this request is 50.

```c#
var request = new MessageRequest
{
    Limit = 20
};
var messages = await gitterApiService.GetRoomMessagesAsync("room-id", request);
```

Of course, you can overload this parameter like this. Here, we set the *limit* to 20.

```c#
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

```c#
var message = await gitterApiService.SendMessageAsync("room-id", "this is a test message");
```

### [Update message](https://developer.gitter.im/docs/messages-resource#update-a-message)

Update an existing message.

```c#
var message = await gitterApiService.UpdateMessageAsync("room-id", "message-id", "this is an updated message");
```

## Events

```c#
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

```c#
var events = await gitterApiService.GetRoomEventsAsync("room-id");
```

## Groups

```c#
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

```c#
var groups = await gitterApiService.GetGroupsAsync("room-id");
```

### [List of rooms in a group](https://developer.gitter.im/docs/groups-resource#list-rooms-under-group)

```c#
var rooms = await gitterApiService.GetGroupRoomsAsync("group-id");
```

### Create room

The user can create a room, generally a channel, based on a name and other parameters.

```c#
public class CreateRoomRequest
{
    public string Name { get; set; }
    public string Topic { get; set; }
    public bool AddBadge { get; set; }
}
```

```c#
var request = new CreateRoomRequest { Name = "test" };
var room = await gitterApiService.CreateRoomAsync("group-id", request);
```

## Search

### Search rooms

Search rooms by name.

```c#
var results = await gitterApiService.SearchRoomsAsync("test", 50);
```

### Search users

Search users by name.

```c#
var results = await gitterApiService.SearchUsersAsync("test", 50);
```

### Search user repositories

Search repositories of a user.

```c#
var results = await gitterApiService.SearchUserRepositoriesAsync("user-id", "test", 50);
```

## Analytics

### Get room messages count by day

This method is mainly use to build a heatmap of room messages.

```c#
var datesWithCount = await gitterApiService.GetRoomMessagesCountByDayAsync("room-id");
```