namespace MWA.Models
{
    public class UserAlertPreference
    {
        public string Id { get; set; }  // MongoDB _id
        public string UserId { get; set; }
        public double TempThreshold { get; set; } = 35;
        public double WindThreshold { get; set; } = 25;

        public bool EmailAlertsEnabled { get; set; } = true;
    }
}