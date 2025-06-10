using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MWA.Models
{
    public class FavoriteCity
    {
        public string CityName { get; set; }
        public string WeatherDetails { get; set; }
        public string UserId { get; set; }
    }
}
