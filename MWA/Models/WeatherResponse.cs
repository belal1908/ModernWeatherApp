namespace MWA.Models
{
    public class WeatherResponse
    {
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }

        public required string Main { get; set; }
        public required string Description { get; set; }
    }
}