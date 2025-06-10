using System;
using System.Threading.Tasks;

namespace MWA.Services
{
public class AuthService
{
    private readonly Supabase.Client? _supabaseClient;

    public AuthService(Supabase.Client? supabaseClient)
    {
        _supabaseClient = supabaseClient ?? throw new ArgumentNullException(nameof(supabaseClient), "Supabase client is not initialized.");
    }

    public async Task<string?> SignIn(string email, string password)
    {
        if (_supabaseClient == null)
        { 
            return "Authentication service is unavailable.";
        }

        var response = await _supabaseClient.Auth.SignIn(email, password);
        return response != null ? null : "Invalid login credentials.";
    }

    

    public async Task<bool> SignUp(string email, string password)
    {
        if (_supabaseClient == null)
        {
            return false;
        }

        var response = await _supabaseClient.Auth.SignUp(email, password);
        return response.User != null;
    }
}
}