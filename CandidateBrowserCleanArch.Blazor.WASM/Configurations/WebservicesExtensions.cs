using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices;
using CandidateBrowserCleanArch.Blazor.WASM.Statics;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace CandidateBrowserCleanArch.Blazor.WASM.Configurations
{
    public static class WebservicesExtensions
    {
        public static void AddWebServices(this IServiceCollection services) 
        {
            services.AddHttpClient<ICandidateBrowserWebAPIClient, CandidateBrowserWebAPIClient>(opt =>
            {
                opt.BaseAddress = new Uri(UrlStatics._azureAPIHostUrl);
                opt.EnableIntercept(services.BuildServiceProvider());
            });

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICandidatesService, CandidatesService>();
            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddScoped<ICompaniesService, CompaniesService>();
            services.AddScoped<ICandidateCompanyService, CandidateCompanyService>();
            services.AddScoped<ICandidateProjectService, CandidateProjectService>();
        }
    }
}
