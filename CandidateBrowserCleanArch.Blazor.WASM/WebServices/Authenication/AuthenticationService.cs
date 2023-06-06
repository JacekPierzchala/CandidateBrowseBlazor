using AutoMapper;
using CandidateBrowserCleanArch.Blazor.WASM.Providers;
using CandidateBrowserCleanArch.Blazor.WASM.Statics;
using CandidateBrowserCleanArch.Blazor.WASM.ViewModels;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


namespace CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IMapper _mapper;
        private readonly IWebAssemblyHostEnvironment _hostEnvironment;     
        private readonly string _redirect_uri;

        public AuthenticationService(
            AuthenticationStateProvider authenticationStateProvider,
            ICandidateBrowserWebAPIClient client, ITokenService tokenService, IConfiguration configuration,
            IMapper mapper, IWebAssemblyHostEnvironment hostEnvironment)
            : base(client, tokenService, configuration)
        {
           _authenticationStateProvider = authenticationStateProvider;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;

            _redirect_uri = $"{_hostEnvironment.BaseAddress}" + UrlStatics.signinGoogle;
        }

        public async Task<Response<AuthResponse>> AuthenticateAsync(LoginViewModel loginViewModel)
        {
            var userDto = _mapper.Map<AuthRequest>(loginViewModel);
            Response<AuthResponse> response;
            try
            {
                var result = await _client.LoginAsync(ApiVersion, userDto);
                response = new Response<AuthResponse>
                {
                    Data = result,
                    Success = true
                };
                await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn(result.Token,result.RefreshToken);
            }
            catch (ApiException ex)
            {               
                response = ConvertApiExceptions<AuthResponse>(ex);
            }
            return response;
        }

        public async Task<Response<AuthResponse>> AuthenticateByGoogleAsync(string url)
        {
            Response<AuthResponse> response= new();
   
            try
            {              
                var code=await _tokenService.PrepareAuthCode(url);
                var result = await _client.LoginGoogleAsync(code, _redirect_uri, ApiVersion);
                if(result.Success)
                {
                    response = new Response<AuthResponse>
                    {
                        Data = result,
                        Success = true
                    };
                    await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn(result.Token, result.RefreshToken);
                }

            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<AuthResponse>(ex);
            }
            return response;
        
        }

        public async Task<Response<string>> GetGoogleAuthUrlAsync()
        {
            Response<string> response= new();
            var result = await _client.GetGoogleRedirectUrlAsync(_redirect_uri, ApiVersion);
            if(result.Success )
            {
                response.Data = result.Data;
                response.Success=true;
            }
            else
            {
                response.Data = _hostEnvironment.BaseAddress;
                response.Success=false; 
                response.Message=result.Message;
            }
            return response;
        }

        public async Task LogOut()
        {
            //_userSettingsProvider.
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
        }

        public async Task<Response<AuthResponse>> RefreshToken()
        {
            Response<AuthResponse> response;
            try
            {
                var storedTokens= await _tokenService.GetTokenAsync();
                var request = new RefreshTokenRequest
                {
                    RefreshToken = storedTokens.refreshToken,
                    Token = storedTokens.token
                };

                var result = await _client.RefreshAsync(ApiVersion, request);
                response = new Response<AuthResponse>
                {
                    Data = result,
                    Success = result.Success
                };
                if(response.Success)
                {
                    await _tokenService.SetTokenAsync(response.Data.Token,response.Data.RefreshToken);
                }
                else
                {
                   await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
                }

            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<AuthResponse>(ex);
                await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
            }
            return response;
        }

        public async Task<Response<RegistrationResponse>> RegisterAsync(RegisterViewModel registerViewModel)
        {
            Response<RegistrationResponse> response;
            var userDto = _mapper.Map<RegistrationRequest>(registerViewModel);
            userDto.ReturnUrl = _hostEnvironment.BaseAddress;
            try
            {
                var result = await _client.RegisterAsync(ApiVersion, userDto);
                response = new()
                {
                    Data = result,
                    Success = true,
                    Message="Registration process success"
                };
             
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<RegistrationResponse>(ex);
            }
            return response;      
        }

        public async Task<Response<bool>> ResendConfirmationAsync(ResendConfirmationViewModel resendViewModel)
        {
            resendViewModel.ReturnUrl = _hostEnvironment.BaseAddress;
            Response<bool> response;
            try
            {
                var result = await _client.ConfirmEmailRepeatAsync(ApiVersion, _mapper.Map<ConfirmEmailRepeatRequest>(resendViewModel));
                response = new()
                {
                    Success = result.Success,
                    Message = $"Confirmation link has been resent to:{resendViewModel.Email}"
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<bool>(ex);
            }
            return response;

        }

        public async Task<Response<bool>> ForgotPasswordAsync(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            forgotPasswordViewModel.ReturnUrl = $"{_hostEnvironment.BaseAddress}{UrlStatics.resetpassword}?";
            Response<bool> response;
            try
            {
                var result = await _client.ForgotPasswordAsync(ApiVersion, _mapper.Map<ForgotPasswordRequest>(forgotPasswordViewModel));
                response = new()
                {
                    Success = result.Success,
                    Message = $"Reset password link has been sent to:{forgotPasswordViewModel.Email}"
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<bool>(ex);
            }
            return response;
   
        }
        public async Task<Response<bool>> ResetPasswordAsync(ResetPasswordViewModel viewModel)
        {
            Response<bool> response;
            try
            {
                var result = await _client.ResetPasswordAsync(ApiVersion, _mapper.Map<ResetPasswordRequest>(viewModel));
                response = new()
                {
                    Success = result.Success,
                    Message=result.Message
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<bool>(ex);
            }
            return response;

        }
    }
}
