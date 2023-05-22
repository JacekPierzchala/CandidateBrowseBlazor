using AutoMapper;
using CandidateBrowserCleanArch.Blazor.WASM.ViewModels;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

namespace CandidateBrowserCleanArch.Blazor.WASM.Mappers
{
    public class CandidateProfile:Profile
    {
        public CandidateProfile()
        {
            CreateMap<CandidateDetailsDto,CandidateEditViewModel>()
                .ForMember(c=>c.DateOfBirth,map=>map.MapFrom(ca=>ca.DateOfBirth.Date))    
                .ForMember(c=>c.ProfilePictureOld,map=>map.MapFrom(ca=>ca.ProfilePicture))
                .ReverseMap();
            CreateMap<CandidateEditViewModel,CandidateUpdateDto>()
                 .ForMember(c => c.DateOfBirth, map => map.MapFrom(ca => ca.DateOfBirth.Date))
                .ReverseMap();
        }
    }
}
