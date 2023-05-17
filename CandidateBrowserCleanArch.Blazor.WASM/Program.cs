using Blazored.LocalStorage;
using Blazored.SessionStorage;
using CandidateBrowserCleanArch.Blazor.WASM;
using CandidateBrowserCleanArch.Blazor.WASM.Providers;
using CandidateBrowserCleanArch.Blazor.WASM.Services;
using CandidateBrowserCleanArch.Blazor.WASM.StateContainers;
using CandidateBrowserCleanArch.Blazor.WASM.Statics;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();

builder.Services.AddHttpClient<ICandidateBrowserWebAPIClient, CandidateBrowserWebAPIClient>(opt =>
{
    opt.BaseAddress = new Uri(UrlStatics._azureAPIHostUrl);
    opt.EnableIntercept(builder.Services.BuildServiceProvider());
});




builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(opt =>
    opt.GetRequiredService<ApiAuthenticationStateProvider>());


builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<RefreshTokenService>();
builder.Services.AddScoped<HttpInterceptorService>();

builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<DialogService>();

builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddScoped<ICandidatesService, CandidatesService>();
builder.Services.AddScoped<IProjectsService, ProjectsService>();
builder.Services.AddScoped<ICompaniesService, CompaniesService>();


builder.Services.AddScoped<CandidateSearchStateContainer>();

builder.Services.AddAuthorizationCore(options=>options.AddPolicy(PermissionStatics.CandidateRead, policy =>
                    policy.RequireClaim(CustomClaimTypes.Permission, PermissionStatics.CandidateRead)));


builder.Services.AddHttpClientInterceptor();
await builder.Build().RunAsync();


