using AutoMapper;
using AutoMapper.Execution;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;
using System.ComponentModel.Design;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public class CandidateProjectProfile: Profile
{
    public CandidateProjectProfile()
    {
        CreateMap<CandidateProjectEditModel, CandidateProjectAddDto>()
         //.ForMember(c => c.ProjectId, map => map.MapFrom(ca =>ca.ProjectId))
        .ReverseMap();
        CreateMap<CandidateProjectDto, CandidateProjectEditModel>()
                 .ForMember(destination => destination.ProjectId, member => member.MapFrom(source => source.Project.Id))
                .ReverseMap();
        CreateMap<CandidateProjectEditModel, CandidateProjectUpdateDto>()
                  //.ForMember(c => c.CompanyId, map => map.MapFrom(ca => (int)ca.CompanyId))
                .ReverseMap();
    }

}
