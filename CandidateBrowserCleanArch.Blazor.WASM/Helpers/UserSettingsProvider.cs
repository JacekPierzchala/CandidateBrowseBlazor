using Blazored.LocalStorage;
using CandidateBrowserCleanArch.Blazor.WASM.Statics;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public class UserSettingsProvider: IUserSettingsProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IUserSettingsService _userSettingsService;
    private List<ConfigThemeDto> _themes;
    public string SelectedTheme { get; set; } 

    public event Action<string>? SelectedThemeChange;

    public UserSettingsProvider(ILocalStorageService localStorageService,
        IUserSettingsService userSettingsService)
    {
        _localStorageService = localStorageService;
        _userSettingsService = userSettingsService;
    }

    public async Task UpdateThemeAsync(int theme, string userId)
    {
        var selectedTheme = _themes.FirstOrDefault(c => c.Id == theme);
        if(selectedTheme != null) 
        {
            SelectedTheme = selectedTheme.Theme;
        }
        else
        {
             SelectedTheme= ThemeStatics.Light;
        }
        var settings = new UserSettingsDto
        {
            UserId = userId,
            ConfigThemeId = theme
        };
        await _userSettingsService.UpdateUserSettings(settings);

        await _localStorageService.RemoveItemAsync(ThemeStatics.Theme);
        await _localStorageService.SetItemAsStringAsync(ThemeStatics.Theme, SelectedTheme);
        SelectedThemeChange?.Invoke(SelectedTheme);
    }
    public async Task GetTheme(string? userId)
    {
        if(!string.IsNullOrEmpty(userId))
        {
            SelectedTheme = await _localStorageService.GetItemAsStringAsync(ThemeStatics.Theme);
            if (SelectedTheme == null)
            {
                await GetAllThemesAsync();
                var resposne = await _userSettingsService.GetUserSettings(userId);
                if (resposne.Success && resposne.Data!=null)
                {
                    SelectedTheme = _themes.FirstOrDefault(c => c.Id == resposne.Data.ConfigThemeId)?.Theme;
                    await _localStorageService.SetItemAsStringAsync(ThemeStatics.Theme, SelectedTheme);
                }

            }
        }
        
        SelectedTheme = SelectedTheme != null ? SelectedTheme : ThemeStatics.Light;
        
        SelectedThemeChange?.Invoke(SelectedTheme);
    }

    public async Task<IEnumerable<ConfigThemeDto>> GetAllThemesAsync()
    {
        var  response=await _userSettingsService.GetThemesAsync();
        if(response.Success)
        {
            _themes= response.Data.ToList();
        }
        else
        {
            _themes = new List<ConfigThemeDto>();
        }
        return _themes;
    }

    public async Task ClearThemes()
    {
        await _localStorageService.RemoveItemAsync(ThemeStatics.Theme);
        SelectedTheme = ThemeStatics.Light;
        SelectedThemeChange?.Invoke(SelectedTheme);
    }
}
