using System.Net.Http.Headers;
using Toolbelt.Blazor;

namespace CandidateBrowserCleanArch.Blazor.WASM.Services;

public class HttpInterceptorService
{
    private readonly HttpClientInterceptor _interceptor;
    private readonly RefreshTokenService _refreshTokenService;

    public HttpInterceptorService(HttpClientInterceptor interceptor,
        RefreshTokenService refreshTokenService)
    {
        _interceptor = interceptor;
        _refreshTokenService = refreshTokenService;
    }
    public void RegisterEvent()=> _interceptor.BeforeSendAsync += InterceptorBeforeSendAsync;

    public async Task InterceptorBeforeSendAsync(object sender, HttpClientInterceptorEventArgs e)
    {
        var absPath = e.Request.RequestUri.AbsolutePath;
        if (!absPath.ToLower().Contains(TokenStatics.Token) && !absPath.ToLower().Contains(TokenStatics.Auth))
        {
            var token = await _refreshTokenService.TryRefreshToken();
            if (!string.IsNullOrEmpty(token))
            {
                e.Request.Headers.Authorization = new AuthenticationHeaderValue(TokenStatics.Bearer, token);
            }
        }
    }

    public void DisposeEvent() => _interceptor.BeforeSendAsync -= InterceptorBeforeSendAsync;
}
