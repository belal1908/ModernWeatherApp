using System.Net.Http; 
using MWA.Components;
using MWA.Services;
using Microsoft.AspNetCore.Builder;
using Supabase;
using Supabase.Gotrue;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<HttpClient>(sp =>
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri("https://api.openweathermap.org")
    };
    return httpClient;
});

var supabaseUrl = "https://lggneeqnofsjwrifizhq.supabase.co"; 
var supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImxnZ25lZXFub2ZzandyaWZpemhxIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDA1NTE3NjUsImV4cCI6MjA1NjEyNzc2NX0.eMgNm_nMqAEVwyoTx1-jGQQFL2-s2r0fCut22SeIEEM";               // Replace with your actual Supabase Key

var options = new Supabase.SupabaseOptions
{
    AutoConnectRealtime = true
};

var supabaseClient = new Supabase.Client(supabaseUrl, supabaseKey, options);
builder.Services.AddSingleton(supabaseClient);

builder.Services.AddScoped<UserSessionService>();

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