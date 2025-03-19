using System;
using System.Threading.Tasks;

namespace MWA.Services
{
    public class UserStateService
    {
        public string? UserId { get; private set; }
        public string? UserEmail { get; private set; }
        public bool IsAuthenticated => !string.IsNullOrEmpty(UserId);

        public void SetUser(string userId, string userEmail)
        {
            UserId = userId;
            UserEmail = userEmail;
        }

        public void ClearUser()
        {
            UserId = null;
            UserEmail = null;
        }
    }
}