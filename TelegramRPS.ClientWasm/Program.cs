using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TelegramRPS.ClientWasm;
using TelegramRPS.ClientWasm.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://api.telegramrps.ru/") });
builder.Services.AddScoped<TelegramInterop>();


builder.Services.AddScoped<TelegramAuthService>();
builder.Services.AddScoped<UserProfileService>();

builder.Services.AddScoped<GameHubService>();
builder.Services.AddScoped<AuthHubService>();

await builder.Build().RunAsync();
