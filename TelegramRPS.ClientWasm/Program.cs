using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TelegramRPS.Client.Services.SignalR;
using TelegramRPS.ClientWasm;
using TelegramRPS.ClientWasm.Layout;
using TelegramRPS.ClientWasm.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://api.telegramrps.ru/") });

builder.Services.AddScoped(sp =>
{
    var client = new HttpClient { BaseAddress = new Uri("https://api.telegramrps.ru/") };
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    return client;
});

// Чтобы клиент понимал enum как строку
builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddScoped<TelegramInterop>();

builder.Services.AddScoped<TelegramAuthService>();
builder.Services.AddScoped<UserProfileService>();
builder.Services.AddScoped<MainLayout>();

builder.Services.AddScoped<LobbyHubClient>();
builder.Services.AddScoped<AuthHubService>();

await builder.Build().RunAsync();
