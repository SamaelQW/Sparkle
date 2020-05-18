namespace Sparkle.Domain.Core
{
    public class GlobalConstants
    {
        public const string PostView = nameof(PostView);

        public const string FriendRelation = "In_Friend";

        public class PostViewConstants
        {
            public const string UserPosts = nameof(UserPosts);

            public const string FriendPosts = nameof(FriendPosts);
        }
    }
}
