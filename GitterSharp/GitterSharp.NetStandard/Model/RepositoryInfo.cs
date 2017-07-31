using Newtonsoft.Json;

namespace GitterSharp.Model
{
    public class RepositoryInfo
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("owner")]
        public GitHubUser Owner { get; set; }

        [JsonProperty("private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("html_url")]
        public string WebsiteUrl{ get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("fork")]
        public bool IsFork { get; set; }

        [JsonProperty("url")]
        public string ApiUrl { get; set; }

        [JsonProperty("forks_url")]
        public string ForksApiUrl { get; set; }

        [JsonProperty("keys_url")]
        public string KeysApiUrl { get; set; }

        [JsonProperty("collaborators_url")]
        public string CollaboratorsApiUrl { get; set; }

        [JsonProperty("teams_url")]
        public string TeamsApiUrl { get; set; }

        [JsonProperty("hooks_url")]
        public string HooksApiUrl { get; set; }

        [JsonProperty("issue_events_url")]
        public string IssueEventsApiUrl { get; set; }

        [JsonProperty("events_url")]
        public string EventsApiUrl { get; set; }

        [JsonProperty("assignees_url")]
        public string AssigneesApiUrl { get; set; }

        [JsonProperty("branches_url")]
        public string BranchesApiUrl { get; set; }

        [JsonProperty("tags_url")]
        public string TagsApiUrl { get; set; }

        [JsonProperty("blobs_url")]
        public string BlobsApiUrl { get; set; }

        [JsonProperty("git_tags_url")]
        public string GitTagsApiUrl { get; set; }

        [JsonProperty("git_refs_url")]
        public string GitRefsApiUrl { get; set; }

        [JsonProperty("trees_url")]
        public string TreesApiUrl { get; set; }

        [JsonProperty("statuses_url")]
        public string StatusesApiUrl { get; set; }

        [JsonProperty("languages_url")]
        public string LanguagesApiUrl { get; set; }

        [JsonProperty("stargazers_url")]
        public string StargazersApiUrl { get; set; }

        [JsonProperty("contributors_url")]
        public string ContributorsApiUrl { get; set; }

        [JsonProperty("subscribers_url")]
        public string SubscribersApiUrl { get; set; }

        [JsonProperty("subscription_url")]
        public string SubscriptionApiUrl { get; set; }

        [JsonProperty("commits_url")]
        public string CommitsApiUrl { get; set; }

        [JsonProperty("git_commits_url")]
        public string GitCommitsApiUrl { get; set; }

        [JsonProperty("comments_url")]
        public string CommentsApiUrl { get; set; }

        [JsonProperty("issue_comment_url")]
        public string IssueCommentsApiUrl { get; set; }

        [JsonProperty("contents_url")]
        public string ContentsApiUrl { get; set; }

        [JsonProperty("compare_url")]
        public string CompareApiUrl { get; set; }

        [JsonProperty("merges_url")]
        public string MergesApiUrl { get; set; }

        [JsonProperty("archive_url")]
        public string ArchiveApiUrl { get; set; }

        [JsonProperty("downloads_url")]
        public string DownloadsApiUrl { get; set; }

        [JsonProperty("issues_url")]
        public string IssuesApiUrl { get; set; }

        [JsonProperty("pulls_url")]
        public string PullsApiUrl { get; set; }

        [JsonProperty("milestones_url")]
        public string MilestonesApiUrl { get; set; }

        [JsonProperty("notifications_url")]
        public string NotificationsApiUrl { get; set; }

        [JsonProperty("labels_url")]
        public string LabelsApiUrl { get; set; }

        [JsonProperty("releases_url")]
        public string ReleasesApiUrl { get; set; }

        [JsonProperty("deployments_url")]
        public string DeploymentsApiUrl { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("pushed_at")]
        public string PushedAt { get; set; }

        [JsonProperty("git_url")]
        public string GitUrl { get; set; }

        [JsonProperty("ssh_url")]
        public string SshUrl { get; set; }

        [JsonProperty("clone_url")]
        public string CloneUrl { get; set; }

        [JsonProperty("svn_url")]
        public string SvnUrl { get; set; }

        [JsonProperty("homepage")]
        public string HomepageUrl { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("stargazers_count")]
        public int StargazersCount { get; set; }

        [JsonProperty("watchers_count")]
        public int WatchersCount { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("has_issues")]
        public bool HasIssues { get; set; }

        [JsonProperty("has_projects")]
        public bool HasProjects { get; set; }

        [JsonProperty("has_downloads")]
        public bool HasDownloads { get; set; }

        [JsonProperty("has_wiki")]
        public bool HasWiki { get; set; }

        [JsonProperty("has_pages")]
        public bool HasPages { get; set; }

        [JsonProperty("forks_count")]
        public int ForksCount { get; set; }

        [JsonProperty("mirror_url")]
        public string MirrorUrl { get; set; }

        [JsonProperty("open_issues_count")]
        public int OpenIssuesCount { get; set; }

        [JsonProperty("forks")]
        public int Forks { get; set; }

        [JsonProperty("open_issues")]
        public int OpenIssues { get; set; }

        [JsonProperty("watchers")]
        public int Watchers { get; set; }

        [JsonProperty("default_branch")]
        public string DefaultBranch { get; set; }

        [JsonProperty("permissions")]
        public GitHubPermission Permissions { get; set; }

        [JsonProperty("allow_squash_merge")]
        public bool AllowSquashMerge { get; set; }

        [JsonProperty("allow_merge_commit")]
        public bool AllowMergeCommit { get; set; }

        [JsonProperty("allow_rebase_merge")]
        public bool AllowRebaseMerge { get; set; }

        [JsonProperty("organization")]
        public GitHubUser Organization { get; set; }

        [JsonProperty("network_count")]
        public int NetworkCount { get; set; }

        [JsonProperty("subscribers_count")]
        public int SubscribersCount { get; set; }
    }
}
