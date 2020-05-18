using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Sparkle.Domain.Entities
{
    public class Comment
    {
        /// <summary>
        /// The comment's body
        /// </summary>
        [BsonElement("body")]
        public string Body { get; set; }

        /// <summary>
        /// The name of <see cref="User"/> which wrote this comment
        /// </summary>
        [BsonElement("ownerName")]
        public string OwnerName { get; set; }

        /// <summary>
        /// The surnname of <see cref="User"/> which wrote this comment
        /// </summary>
        [BsonElement("ownerSurname")]
        public string OwnerSurname { get; set; }

        /// <summary>
        /// The Owner UserName
        /// </summary>
        [BsonElement("ownerUserName")]
        public string OwnerUserName { get; set; }

        /// <summary>
        /// The comment created date
        /// </summary>
        [BsonElement("created")]
        public DateTime CreatedDate { get; set; }
    }
}
