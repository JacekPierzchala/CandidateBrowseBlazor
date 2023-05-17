using AutoMapper;
using CandidateBrowserCleanArch.Blazor.WASM.ViewModels;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

namespace CandidateBrowserCleanArch.Blazor.WASM.Mappers
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<AuthRequest, LoginViewModel>().ReverseMap();
            CreateMap<RegistrationRequest, RegisterViewModel>().ReverseMap();
            CreateMap<ConfirmEmailRepeatRequest, ResendConfirmationViewModel>().ReverseMap();
            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>().ReverseMap();
            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>().ReverseMap();
        }
    }
}
