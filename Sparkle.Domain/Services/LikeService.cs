using MongoDB.Driver;
using Sparkle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sparkle.Domain.Services
{

    /// <summary>
    /// The like service. Incapsulate logic about add or remove likes under post
    /// </summary>
    public class LikeService
    {
        #region Private Members
        private readonly PostService _postService;

        #endregion


        #region Constructor

        public LikeService(PostService service)
        {
            _postService = service;
        }
        #endregion


        /// <summary>
        /// Add new like or delete already existed if like already pressed
        /// </summary>
        /// <param name="postId">Liked post id</param>
        /// <param name="username">User which pressed like</param>
        /// <returns></returns>
        public async Task LikeAsync(string postId, string username)
        {
            var like = new Like()
            {
                Liked = DateTime.Now,
                OwnerUsername = username
            };
            var post = await _postService.GetAsync(postId);
            Task task;
            if (post.Likes == null)
            {
                post.Likes = new List<Like>();
                task = AddLikeAsync(post, like);
                await _postService.UpdateAsync(postId, post);
                return;
            }

            if (post.Likes.Any(l => l.OwnerUsername == username))
            {
                task = RemoveLikeAsync(post, like);
            }
            else
            {
                task = AddLikeAsync(post, like);
            }

            Task.WaitAll(task);
            await _postService.UpdateAsync(postId, post);
        }

        #region Helper Methods

        /// <summary>
        /// Remove like if it already exists
        /// </summary>
        /// <param name="post"></param>
        /// <param name="like"></param>
        /// <returns></returns>
        private Task RemoveLikeAsync(Post post, Like like)
        {
            return Task.Run(() =>
            {
                var likeToRemove = post.Likes.First(l => l.OwnerUsername == like.OwnerUsername);
                post.Likes.Remove(likeToRemove);
            });
        }

        /// <summary>
        /// Add like 
        /// </summary>
        /// <param name="post"></param>
        /// <param name="like"></param>
        /// <returns></returns>
        private Task AddLikeAsync(Post post, Like like)
        {
            return Task.Run(() =>
            {
                post.Likes.Add(like);
            });
        }

        #endregion


    }

}
