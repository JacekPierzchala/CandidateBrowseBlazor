using AutoMapper;
using CandidateBrowserCleanArch.Blazor.WASM.ViewModels;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public class CandidateCompanyProfile: Profile
{
	public CandidateCompanyProfile()
	{
        CreateMap<CandidateCompanyEditViewModel, CandidateCompanyAddDto>()
             .ForMember(c => c.CompanyId, map => map.MapFrom(ca => (int)ca.CompanyId))
            .ReverseMap();
        CreateMap<CandidateCompanyDto, CandidateCompanyEditViewModel>()
              .ForMember(destination => destination.CompanyId, member => member.MapFrom(source => source.Company.Id))
             .ForMember(c => c.DateStart, map => map.MapFrom(ca => ca.DateStart.DateTime))
             .ForMember(c => c.DateEnd, map => map.MapFrom(ca =>  ca.DateEnd.Value.DateTime))
            .ReverseMap();
        CreateMap<CandidateCompanyEditViewModel, CandidateCompanyUpdateDto>()
              .ForMember(c => c.CompanyId, map => map.MapFrom(ca => (int)ca.CompanyId))
            .ReverseMap();
    }
}
