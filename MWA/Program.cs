using System.Net.Http; 
using MWA.Components;
using MWA.DBConnections;
using MWA.Models;
using MWA.Services;
using Microsoft.AspNetCore.Builder;
using MudBlazor.Services;
using Supabase;
using Supabase.Gotrue;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// MongoDB Configuration
var mongoConfig = builder.Configuration.GetSection("MongoDB");
string connectionString = mongoConfig["ConnectionString"];
string databaseName = mongoConfig["DatabaseName"];

if (string.IsNullOrEmpty(connectionString))
{
    throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty.");
}

if (string.IsNullOrEmpty(databaseName))
{
    throw new ArgumentNullException(nameof(databaseName), "Database name cannot be null or empty.");
}

// Service Configuration
builder.Services.AddScoped(sp => new MongoHelper(connectionString, databaseName));
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://your-api-base-url/") });
builder.Services.AddRazorPages();
builder.Services.AddMudServices();



builder.Services.AddSingleton<HttpClient>(sp =>
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri("https://api.openweathermap.org")
    };
    return httpClient;
});

var supabaseUrl = builder.Configuration["Supabase:Url"]!; 
var supabaseKey = builder.Configuration["Supabase:Key"]!;           

var options = new Supabase.SupabaseOptions
{
    AutoConnectRealtime = true
};

var supabaseClient = new Supabase.Client(supabaseUrl, supabaseKey, options);
builder.Services.AddSingleton(supabaseClient);

builder.Services.AddScoped<WeatherService>();

builder.Services.AddSingleton<UserStateService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

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