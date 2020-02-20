using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Sparkle.Domain.Entities
{

    /// <summary>
    /// Entity which represent Post in Social Network
    /// </summary>
    public class Post : IEntity
    {
        /// <summary>
        /// The post id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The post title
        /// </summary>
        [BsonElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// The post body
        /// </summary>
        [BsonElement("body")]
        public string Body { get; set; }


        /// <summary>
        /// The post category
        /// </summary>
        [BsonElement("category")]
        public string Category { get; set; }

        /// <summary>
        /// The post created date
        /// </summary>
        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
