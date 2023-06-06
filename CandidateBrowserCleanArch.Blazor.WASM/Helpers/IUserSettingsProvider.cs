using CandidateBrowserCleanArch.Blazor.WASM.Statics;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public interface IUserSettingsProvider
{
    event Action<string>? SelectedThemeChange;
    public string SelectedTheme { get; set; }
    Task UpdateThemeAsync(int theme, string userId);

    Task GetTheme(string? userId);

    Task<IEnumerable<ConfigThemeDto>> GetAllThemesAsync();
    Task ClearThemes();
}
