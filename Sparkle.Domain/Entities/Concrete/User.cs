﻿using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sparkle.Domain.Entities
{
    /// <summary>
    /// Entity which represent User
    /// </summary>
    public class User : IEntity
    {
        /// <summary>
        /// The user id in DataBase
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }


        /// <summary>
        /// The user username or login
        /// </summary>
        [BsonElement("username")]
        public string UserName { get; set; }

        /// <summary>
        /// The user password
        /// </summary>
        [BsonElement("password")]
        public string Password { get; set; }

        /// <summary>
        /// The user E-mail
        /// </summary>
        [BsonElement("email")]
        public string Email { get; set; }

        /// <summary>
        /// The user name
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// The user surname
        /// </summary>
        [BsonElement("surname")]
        public string Surname { get; set; }

        /// <summary>
        /// The user age
        /// </summary>
        [BsonElement("age")]
        public int Age { get; set; }

        /// <summary>
        /// The user's date of births 
        /// </summary>
        [BsonElement("dateOfBirth")]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// User's posts IDs 
        /// </summary>
        [BsonElement("userPosts")]
        public IEnumerable<string> PostIds { get; set; }

        /// <summary>
        /// The user status in network
        /// </summary>
        [BsonElement("userStatus")]
        public EUserStatus Status { get; set; }


        /// <summary>
        /// The user's friends
        /// </summary>
        [BsonElement("friends")]
        public IEnumerable<Friend> Friends { get; set; }




    }
}
