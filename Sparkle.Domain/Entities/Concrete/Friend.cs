using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sparkle.Domain.Entities
{
    public class Friend
    {
        [BsonElement("friendId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FriendId { get; set; }
        
        /// <summary>
        /// The friend's name
        /// </summary>
        [BsonElement("friendName")]
        public string Name { get; set; }
        
        /// <summary>
        /// The friend's surname
        /// </summary>
        [BsonElement("friendSurname")]
        public string Surname { get; set; }

        /// <summary>
        /// The friend username
        /// </summary>
        [BsonElement("friendUsername")]
        public string Username { get; set; }
    }
}
