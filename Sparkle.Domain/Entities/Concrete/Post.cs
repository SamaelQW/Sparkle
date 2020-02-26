using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Sparkle.Domain.Entities
{

    /// <summary>
    /// Entity which represent Post in Social Network
    /// </summary>
    public class Post : IEntity
    {
        /// <summary>
        /// The post's id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The post's title
        /// </summary>
        [BsonElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// The post's body
        /// </summary>
        [BsonElement("body")]
        public string Body { get; set; }


        /// <summary>
        /// The post's category
        /// </summary>
        [BsonElement("category")]
        public string Category { get; set; }

        /// <summary>
        /// The post's created date
        /// </summary>
        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// The post's owner username
        /// </summary>
        [BsonElement("owner")]
        public string OwnerUserName { get; set; }

        /// <summary>
        /// The comments under <see cref="Post"/>
        /// </summary>
        [BsonElement("comments")]
        public List<Comment> Comments { get; set; }

        /// <summary>
        /// Likes under post
        /// </summary>
        [BsonElement("likes")]
        public List<Like> Likes { get; set; }


    }
}
