using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sparkle.Domain.Entities
{
    /// <summary>
    /// Interface which implements all entities in DataBase
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Entity Id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
