using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http;
using TelegramRPS.Shared.Models.Profile;

namespace TelegramRPS.ClientWasm.Services;

public class UserProfileService
{
    private readonly AuthHubService _authHubService;
    private UserProfile? _currentUserProfile;
    private readonly HttpClient _httpClient;

    public event Action? OnUserProfileChanged;

    public UserProfile? CurrentUserProfile
    {
        get => _currentUserProfile;
        private set
        {
            _currentUserProfile = value;
            OnUserProfileChanged?.Invoke();
        }
    }

    public UserProfileService(AuthHubService authHubService, HttpClient httpClient)
    {
        _authHubService = authHubService;
        _httpClient = httpClient;
    }

    public void SetUserProfile(UserProfile userProfile)
    {
        CurrentUserProfile = userProfile;
    }

    public async Task<UserProfile?> AuthenticateAsync(long telegramId, string? firstName, 
                                                      string? lastName, string? username, string? photoUrl)
    {
        var profile = await _authHubService.AuthenticateAsync(telegramId, firstName, lastName, username, photoUrl);
        CurrentUserProfile = profile;
        return profile;
    }

    public async Task<bool> UpdateUserNameAsync(string newName)
    {
        if (CurrentUserProfile == null)
            return false;

        CurrentUserProfile.DisplayName = newName;

        try
        {
            await _authHubService.UpdateUserProfileAsync(CurrentUserProfile);
            OnUserProfileChanged?.Invoke();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateUserProfileAsync(UserProfile profile)
    {
        if (CurrentUserProfile == null)
            return false;

        CurrentUserProfile = profile;

        try
        {
            await _authHubService.UpdateUserProfileAsync(CurrentUserProfile);
            OnUserProfileChanged?.Invoke();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<string?> UploadAvatarAsync(IBrowserFile file)
    {
        var content = new MultipartFormDataContent();
        var streamContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024));
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

        content.Add(streamContent, "avatar", file.Name);

        var response = await _httpClient.PostAsync("api/profile/upload-avatar", content);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        return null;
    }
}
