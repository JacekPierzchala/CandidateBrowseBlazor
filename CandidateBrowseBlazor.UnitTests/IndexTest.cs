using Blazored.LocalStorage;
using Bunit;
using Bunit.Extensions;
using CandidateBrowserCleanArch.Blazor.WASM;
using CandidateBrowserCleanArch.Blazor.WASM.Pages;
using CandidateBrowserCleanArch.Blazor.WASM.StateContainers;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Radzen;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using CandidateBrowserCleanArch.Blazor.WASM.Services;
using Xunit;
using Radzen.Blazor;
using CandidateBrowserCleanArch.Blazor.WASM.Shared.Candidates;

namespace CandidateBrowseBlazor.UnitTests;


public class IndexTest: TestContext
{
    [Fact]
    public void  RenderTest()
    {
        var configuration = new ConfigurationBuilder().Build();
        Services.AddSingleton<IConfiguration>(configuration);
        
        JSInterop.Setup<string>("localStorage.getItem", "accessToken");


        Services.AddAutoMapper(typeof(Program));
 
        Services.AddHttpClient<ICandidateBrowserWebAPIClient, CandidateBrowserWebAPIClient>();
        Services.AddScoped<HttpInterceptorService>();

        Services.AddBlazoredLocalStorage();
        Services.AddScoped<DialogService>();
        Services.AddScoped<CandidateSearchStateContainer>();
        Services.AddScoped<NotificationService>();
        Services.AddScoped<ICandidatesService, CandidatesService>();
        Services.AddHttpClientInterceptor();
        Services.AddScoped<RefreshTokenService>();

        Services.AddScoped<ITokenService, TokenService>();
        Services.AddScoped<IAuthenticationService, AuthenticationService>();

        var candListcomponent = RenderComponent<CandidateBrowserCleanArch.Blazor.WASM.Shared.Candidates.CandidatesList>();

        Assert.NotNull(candListcomponent.Instance);


    }
}

