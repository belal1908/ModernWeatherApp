namespace MWA.Services
{
    public class UserSessionService
    {
        public string LoggedInUserId { get; private set; } = string.Empty;

        public void SetLoggedInUserId(string userId)
        {
            LoggedInUserId = userId;
            Console.WriteLine($"UserSessionService: LoggedInUserId set to {LoggedInUserId}");
        }
    }
}