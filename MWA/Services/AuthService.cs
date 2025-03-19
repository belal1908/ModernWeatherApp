using Supabase;
using Supabase.Gotrue;

public class AuthService
{
    private readonly Supabase.Client _supabaseClient; // ✅ Declare it

    // ✅ Inject it via constructor
    public AuthService(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient ?? throw new ArgumentNullException(nameof(supabaseClient));
    }

    public async Task<string?> SignIn(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return "Email and password are required.";
        }

        var response = await _supabaseClient.Auth.SignIn(email, password);

        if (response.User != null)
        {
            return null; // ✅ Successful login
        }

        return "Invalid credentials!"; // ✅ Return error message
    }

    public async Task<bool> SignUp(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return false; // ✅ Prevent null error
        }

        var response = await _supabaseClient.Auth.SignUp(email, password);
        return response.User != null;
    }
}