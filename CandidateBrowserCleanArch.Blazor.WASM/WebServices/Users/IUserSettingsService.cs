using AutoMapper;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public interface IUserSettingsService
{
    Task<Response<IEnumerable<ConfigThemeDto>>> GetThemesAsync();
    Task<Response<bool>> UpdateUserSettings(UserSettingsDto userSettings);
    Task<Response<UserSettingsDto>> GetUserSettings(string userId);
}

public class UserSettingsService : BaseHttpService, IUserSettingsService
{
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _authenticationService;

    public UserSettingsService
        (ICandidateBrowserWebAPIClient client, 
        ITokenService tokenService, 
        IConfiguration configuration,
        IMapper mapper,
        IAuthenticationService authenticationService) : 
        base(client, tokenService, configuration)
    {
        _mapper = mapper;
        _authenticationService = authenticationService;
    }

    public async Task<Response<IEnumerable<ConfigThemeDto>>> GetThemesAsync()
    {
        var response = new Response<IEnumerable<ConfigThemeDto>>();
        try
        {
            if (await GetBearerTokenAsync())
            {
                var resultApi = await _client.ThemesAsync(ApiVersion);
                response.Success = resultApi.Success;
                response.Data = resultApi.Data;
            }
            else
            {
                await _authenticationService.LogOut();
            }
        }
        catch (ApiException ex)
        {
            response = ConvertApiExceptions<IEnumerable<ConfigThemeDto>>(ex);
        }
        return response;
    }

    public async Task<Response<bool>> UpdateUserSettings(UserSettingsDto userSettings)
    {
        var response = new Response<bool>();
        try
        {
            if (await GetBearerTokenAsync())
            {
                var resultApi = await _client.SettingsPUTAsync(userSettings.UserId, ApiVersion, userSettings);
                response.Success = resultApi.Success;
                response.Data = resultApi.Data;
            }
            else
            {
                await _authenticationService.LogOut();
            }
        }
        catch (ApiException ex)
        {
            response = ConvertApiExceptions<bool>(ex);
        }
        return response;
    }

    public async Task<Response<UserSettingsDto>> GetUserSettings(string userId)
    {
        var response = new Response<UserSettingsDto>();
        try
        {
            if (await GetBearerTokenAsync())
            {
                var resultApi = await _client.SettingsGETAsync(userId, ApiVersion);
                response.Success = resultApi.Success;
                response.Data = resultApi.Data;
            }
            else
            {
                await _authenticationService.LogOut();
            }
        }
        catch (ApiException ex)
        {
            response = ConvertApiExceptions<UserSettingsDto>(ex);
        }
        return response;
    }
}