using System.Net.Http; // Ensure this is included
using MWA.Components;
using MWA.Services;
using Microsoft.AspNetCore.Builder;
using Supabase;
using Supabase.Gotrue;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register HttpClient for OpenWeatherMap API
builder.Services.AddSingleton<HttpClient>(sp =>
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri("https://api.openweathermap.org") // Change to your API base URL if needed
    };
    return httpClient;
});

// ✅ Register Supabase Client
var supabaseUrl = "https://your-supabase-url.supabase.co";  // Replace with your actual Supabase URL
var supabaseKey = "your-anon-or-service-key";               // Replace with your actual Supabase Key

var options = new Supabase.SupabaseOptions
{
    AutoConnectRealtime = true
};

var supabaseClient = new Supabase.Client(supabaseUrl, supabaseKey, options);
builder.Services.AddSingleton(supabaseClient);

// ✅ Register UserSessionService
builder.Services.AddScoped<UserSessionService>();

// ✅ Add services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// ✅ Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();