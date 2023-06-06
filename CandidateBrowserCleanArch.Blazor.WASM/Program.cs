using Blazored.LocalStorage;
using Blazored.SessionStorage;
using CandidateBrowserCleanArch.Blazor.WASM;
using CandidateBrowserCleanArch.Blazor.WASM.Configurations;
using CandidateBrowserCleanArch.Blazor.WASM.Providers;
using CandidateBrowserCleanArch.Blazor.WASM.Services;
using CandidateBrowserCleanArch.Blazor.WASM.StateContainers;
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

builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(opt =>
    opt.GetRequiredService<ApiAuthenticationStateProvider>());


builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<RefreshTokenService>();
builder.Services.AddScoped<HttpInterceptorService>();
builder.Services.AddScoped<IUserSettingsProvider,UserSettingsProvider>();

builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<DialogService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddWebServices();


builder.Services.AddScoped<CandidateSearchStateContainer>();
builder.Services.ConfigureAuthorization();


builder.Services.AddHttpClientInterceptor();
await builder.Build().RunAsync();


