namespace GitterSharp.Model
{
    public static class RoomSecurityType
    {
        /// <summary>
        /// A room that represents a GitHub Organisation
        /// </summary>
        public static string Organisation = "ORG";

        /// <summary>
        /// A room that represents a GitHub Repository
        /// </summary>
        public static string Repository = "REPO";

        /// <summary>
        /// A one-to-one chat
        /// </summary>
        public static string OneToOne = "ONETOONE";

        /// <summary>
        /// A Gitter channel nested under a GitHub Organisation
        /// </summary>
        public static string OrganisationChannel = "ORG_CHANNEL";

        /// <summary>
        /// A Gitter channel nested under a GitHub Repository
        /// </summary>
        public static string RepositoryChannel = "REPO_CHANNEL";

        /// <summary>
        /// A Gitter channel nested under a GitHub User
        /// </summary>
        public static string UserChannel = "USER_CHANNEL";
    }
}
