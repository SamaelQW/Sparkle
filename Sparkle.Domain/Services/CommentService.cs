using Sparkle.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sparkle.Domain.Services
{
    public class CommentService
    {
        private readonly PostService _postService;
        public CommentService(PostService postService)
        {
            _postService = postService;
        }

        public async Task AddCommentAsync(string postId, Comment comment)
        {
            var post = await _postService.GetAsync(postId);

            if (post.Comments == null)
            {
                post.Comments = new List<Comment>();
            }
            post.Comments.Add(comment);

            await _postService.UpdateAsync(post.Id, post);
        }
    }
}
