namespace MWA.Models
{
    public class OpenWeatherResponse
    {
        public WeatherMain Main { get; set; }
        public List<WeatherInfo> Weather { get; set; }
        public WeatherWind Wind { get; set; }
    }

    public class WeatherMain
    {
        public double Temp { get; set; }
        public int Humidity { get; set; }
    }

    public class WeatherInfo
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }

    public class WeatherWind
    {
        public double Speed { get; set; }
    }

    public class WeatherAlert
{
    public string SenderName { get; set; }
    public string Event { get; set; }
    public long Start { get; set; }
    public long End { get; set; }
    public string Description { get; set; }
}
}