using CandidateBrowserCleanArch.Blazor.WASM.Statics;

namespace CandidateBrowserCleanArch.Blazor.WASM.Configurations
{
    public static class AuthorizationExtensions
    {
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy(PermissionStatics.CandidateRead, policy =>
                      policy.RequireClaim(CustomClaimTypes.Permission, PermissionStatics.CandidateRead));
                options.AddPolicy(PermissionStatics.CandidateDelete, policy =>
                     policy.RequireClaim(CustomClaimTypes.Permission, PermissionStatics.CandidateDelete));
                options.AddPolicy(PermissionStatics.CandidateCreate, policy =>
                     policy.RequireClaim(CustomClaimTypes.Permission, PermissionStatics.CandidateCreate));
                options.AddPolicy(PermissionStatics.CandidateUpdate, policy =>
                     policy.RequireClaim(CustomClaimTypes.Permission, PermissionStatics.CandidateUpdate));


                options.AddPolicy(PermissionStatics.UserAssignRole, policy =>
                  policy.RequireClaim(CustomClaimTypes.Permission, PermissionStatics.UserAssignRole));
                options.AddPolicy(PermissionStatics.UserDelete, policy =>
                     policy.RequireClaim(CustomClaimTypes.Permission, PermissionStatics.UserDelete));
                options.AddPolicy(PermissionStatics.UserLock, policy =>
                     policy.RequireClaim(CustomClaimTypes.Permission, PermissionStatics.UserLock));
                options.AddPolicy(PermissionStatics.UserUpdate, policy =>
                     policy.RequireClaim(CustomClaimTypes.Permission, PermissionStatics.UserUpdate));
            });
        }
    }
}
