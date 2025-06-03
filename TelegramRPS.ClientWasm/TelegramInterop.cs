using System.Text.Json;
using Microsoft.JSInterop;
using TelegramRPS.Shared.Models;

namespace TelegramRPS.ClientWasm;

public class TelegramInterop
{
    private readonly IJSRuntime js;

    public TelegramInterop(IJSRuntime js)
    {
        this.js = js;
    }

    public async Task<TelegramAuthData?> GetTelegramUserDataAsync()
    {
        try
        {
            var result = await js.InvokeAsync<JsonElement?>("getTelegramData");

            if (result.HasValue)
            {
                if (result.Value.TryGetProperty("user", out var userJson))
                {
                    try
                    {
                        var user = JsonSerializer.Deserialize<TelegramUser>(userJson.GetRawText());
                        return new TelegramAuthData
                        {
                            InitData = result.Value.GetProperty("initData").GetString() ?? string.Empty,
                            User = user ?? new TelegramUser()
                        };
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Deserialization error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("User property missing in JS result");
                }
            }
            else
            {
                Console.WriteLine("JS result has no value");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JS invoke error: {ex.Message}");
        }

        return null;
    }
}
