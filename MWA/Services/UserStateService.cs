using System;

namespace MWA.Services
{
    public class UserStateService
    {
        public string? UserId { get; private set; }
        public string? UserEmail { get; private set; }

        // Remove one of the IsAuthenticated properties
        public bool IsAuthenticated => CurrentUser != null; // Use CurrentUser to determine authentication

        public User CurrentUser { get; set; }

        public void SetUser(string userId, string userEmail)
        {
            UserId = userId;
            UserEmail = userEmail;
            CurrentUser = new User { Id = userId, Email = userEmail }; // Assuming you have a User class with Id and Email
        }

        public void ClearUser()
        {
            UserId = null;
            UserEmail = null;
            CurrentUser = null; // Clear the CurrentUser as well
        }
    }

public class User
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
}
}