using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitterSharp.Model
{
    public class Room
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("avatarUrl")]
        public string AvatarUrl { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("oneToOne")]
        public bool OneToOne { get; set; }

        [JsonProperty("userCount")]
        public int UserCount { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("unreadItems")]
        public int UnreadItems { get; set; }

        [JsonProperty("mentions")]
        public int UnreadMentions { get; set; }

        [JsonProperty("lastAccessTime")]
        public DateTime LastAccessTime { get; set; }

        [JsonProperty("favourite")]
        public bool Favourite { get; set; }
        
        /// <summary>
        /// Deprecated
        /// </summary>
        [JsonProperty("lurk")]
        public bool DisabledNotifications { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("githubType")]
        public string Type { get; set; }

        /// <summary>
        /// Security of the room
        /// Refers to <see cref="RoomSecurityType"/>
        /// </summary>
        [JsonProperty("security")]
        public string Security { get; set; }

        [JsonProperty("premium")]
        public bool Premium { get; set; }

        [JsonProperty("noindex")]
        public bool NoIndex { get; set; }

        [JsonProperty("tags")]
        public IEnumerable<string> Tags { get; set; }

        [JsonProperty("roomMember")]
        public bool RoomMember { get; set; }

        [JsonProperty("groupId")]
        public string GroupId { get; set; }

        [JsonProperty("public")]
        public bool Public { get; set; }

        [JsonProperty("v")]
        public int Version { get; set; }

        [JsonIgnore]
        public string GitHubUrl { get { return $"https://github.com{Url}"; } }

        /// <summary>
        /// Deprecated (should use `AvatarUrl`)
        /// </summary>
        [JsonIgnore]
        public string Image { get { return AvatarUrl; } }
    }
}
