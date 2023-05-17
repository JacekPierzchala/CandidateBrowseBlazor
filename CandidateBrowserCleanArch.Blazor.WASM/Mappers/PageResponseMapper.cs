using AutoMapper;
using CandidateBrowserCleanArch.Blazor.WASM.StateContainers;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

namespace CandidateBrowserCleanArch.Blazor.WASM.Mappers
{
    public class PageResponseMapper:Profile
    {
        public PageResponseMapper()
        {
            CreateMap<CandidateSearchParameters, CandidateListDtoPagedResultResponse>()
                .ReverseMap();
        }
    }
}
