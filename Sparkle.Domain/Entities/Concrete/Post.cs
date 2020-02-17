using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sparkle.Domain.Entities
{

    /// <summary>
    /// Entity which represent Post in Social Network
    /// </summary>
    public class Post : IEntity
    {
        /// <summary>
        /// Post id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Post titile
        /// </summary>
        [BsonElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// Post category
        /// </summary>
        [BsonElement("category")]
        public string Category { get; set; }

    }
}
