using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MWA.Models
{
    public class AlertLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string UserId { get; set; }
        public string City { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}