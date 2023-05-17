using CandidateBrowserCleanArch.Blazor.WASM.ViewModels;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication
{
    public interface IAuthenticationService
    {
        Task<Response<AuthResponse>> AuthenticateAsync(LoginViewModel loginViewModel);
        Task<Response<RegistrationResponse>> RegisterAsync(RegisterViewModel registerViewModel);
        Task LogOut();
        Task<Response<string>> GetGoogleAuthUrlAsync();
        Task<bool> GetBearerTokenAsync();
        Task<Response<AuthResponse>> RefreshToken();

        Task<Response<AuthResponse>> AuthenticateByGoogleAsync(string url);
        Task<Response<bool>> ResendConfirmationAsync(ResendConfirmationViewModel viewModel);
        Task<Response<bool>> ForgotPasswordAsync(ForgotPasswordViewModel forgotPasswordViewModel);
        Task<Response<bool>> ResetPasswordAsync(ResetPasswordViewModel viewModel);
    }
}
