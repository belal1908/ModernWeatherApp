using Microsoft.AspNetCore.Mvc;
using Supabase;
using System.Threading.Tasks;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly Supabase.Client _supabaseClient;

    public AuthController()
    {
        _supabaseClient = new Supabase.Client(
            "https://lggneeqnofsjwrifizhq.supabase.co",  // Replace with your Supabase API URL
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImxnZ25lZXFub2ZzandyaWZpemhxIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDA1NTE3NjUsImV4cCI6MjA1NjEyNzc2NX0.eMgNm_nMqAEVwyoTx1-jGQQFL2-s2r0fCut22SeIEEM"              // Replace with your Supabase API Key
        );
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        try
        {
            var response = await _supabaseClient.Auth.SignUp(request.Email, request.Password);
            
            if (response.User != null)
                return Ok(new { message = "User registered successfully!" });

            return BadRequest(new { message = "Signup failed!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

public class SignUpRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}