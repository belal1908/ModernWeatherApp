using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MWA.Models
{
    public class FavoriteCity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("CityName")]
        public string CityName { get; set; }

        [BsonElement("UserId")]
        public string UserId { get; set; }

        [BsonElement("WeatherDetails")]
        public object? WeatherDetails { get; set; }
    }
}