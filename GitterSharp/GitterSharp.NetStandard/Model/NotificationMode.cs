namespace GitterSharp.Model
{
    /// <summary>
    /// Notification mode of a room (for a user)
    /// </summary>
    public static class NotificationMode
    {
        /// <summary>
        /// All messages are notified
        /// </summary>
        public static string All = "all";

        /// <summary>
        /// Only mentions and announcements (@/all) are notified
        /// </summary>
        public static string Announcement = "announcement";

        /// <summary>
        /// Only mentions are notified
        /// </summary>
        public static string Mute = "mute";
    }
}
