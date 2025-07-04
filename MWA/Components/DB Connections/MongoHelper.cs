using MWA.Models;
using MongoDB.Driver;

namespace MWA.DBConnections
{
    public class MongoHelper
    {
        private readonly IMongoDatabase _database;

        public MongoHelper(string connectionString, string databaseName)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty.");
            }
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentNullException(nameof(databaseName), "Database name cannot be null or empty.");
            }
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public async Task<bool> IsCityFavoritedAsync(string userId, string cityName)
{
    var collection = _database.GetCollection<FavoriteCity>("FavoriteCities");
    var filter = Builders<FavoriteCity>.Filter.And(
        Builders<FavoriteCity>.Filter.Eq("CityName", cityName),
        Builders<FavoriteCity>.Filter.Eq("UserId", userId)
    );

    return await collection.Find(filter).AnyAsync();
}

        // Property for accessing the "Users" collection
        // public IMongoCollection<User> Users
        // {
        //     get
        //     {
        //         return _database.GetCollection<User>("Users");
        //     }
        // }

        // Method to get favorite cities for a specific user
        public async Task<List<FavoriteCity>> GetFavoriteCitiesAsync(string userId)
        {
            var filter = Builders<FavoriteCity>.Filter.Eq("UserId", userId);
            return await _database.GetCollection<FavoriteCity>("FavoriteCities").Find(filter).ToListAsync();
        }

        // Method to check if a city already exists for a user
        public async Task<bool> CityExistsAsync(string cityName, string userId)
        {
            var collection = _database.GetCollection<FavoriteCity>("FavoriteCities");
            var filter = Builders<FavoriteCity>.Filter.And(
                Builders<FavoriteCity>.Filter.Eq("CityName", cityName),
                Builders<FavoriteCity>.Filter.Eq("UserId", userId)
            );
            return await collection.Find(filter).AnyAsync();
        }

        // Updated method to add a new favorite city with duplicate checking
        public async Task<bool> AddFavoriteCityAsync(FavoriteCity city)
{
    var collection = _database.GetCollection<FavoriteCity>("FavoriteCities");

    var exists = await collection.Find(c =>
        c.CityName == city.CityName &&
        c.UserId == city.UserId).AnyAsync();

    if (exists)
    {
        return false;
    }

    await collection.InsertOneAsync(city);
    return true;
}

        // Method to delete a favorite city from the collection
        public async Task DeleteFavoriteCityAsync(string cityName, string userId)
        {
            var collection = _database.GetCollection<FavoriteCity>("FavoriteCities");
            var filter = Builders<FavoriteCity>.Filter.And(
                Builders<FavoriteCity>.Filter.Eq("CityName", cityName),
                Builders<FavoriteCity>.Filter.Eq("UserId", userId)
            );
            await collection.DeleteOneAsync(filter);
        }



        public async Task AddWeatherAlertAsync(AlertLog alert)
{
    var collection = _database.GetCollection<AlertLog>("WeatherAlerts");
    await collection.InsertOneAsync(alert);
}

public async Task<List<AlertLog>> GetWeatherAlertsAsync(string userId)
{
    var collection = _database.GetCollection<AlertLog>("WeatherAlerts");
    var filter = Builders<AlertLog>.Filter.Eq("UserId", userId);
    return await collection.Find(filter)
        .SortByDescending(a => a.Timestamp)
        .ToListAsync();
}


public async Task DeleteWeatherAlertAsync(string alertId)
{
    var collection = _database.GetCollection<AlertLog>("WeatherAlerts");
    var filter = Builders<AlertLog>.Filter.Eq("_id", alertId);
    await collection.DeleteOneAsync(filter);
}

public async Task DeleteAlertsByCityAsync(string userId, string cityName)
{
    var collection = _database.GetCollection<AlertLog>("WeatherAlerts");
    var filter = Builders<AlertLog>.Filter.And(
        Builders<AlertLog>.Filter.Eq("UserId", userId),
        Builders<AlertLog>.Filter.Eq("City", cityName)
    );
    await collection.DeleteManyAsync(filter);
}

public async Task<UserAlertPreference?> GetUserAlertPreferenceAsync(string userId)
{
    var collection = _database.GetCollection<UserAlertPreference>("UserAlertPreferences");
    return await collection.Find(p => p.UserId == userId).FirstOrDefaultAsync();
}

public async Task SetUserAlertPreferenceAsync(UserAlertPreference preference)
{
    var collection = _database.GetCollection<UserAlertPreference>("UserAlertPreferences");
    var filter = Builders<UserAlertPreference>.Filter.Eq(p => p.UserId, preference.UserId);
    await collection.ReplaceOneAsync(filter, preference, new ReplaceOptions { IsUpsert = true });
}
    }
}