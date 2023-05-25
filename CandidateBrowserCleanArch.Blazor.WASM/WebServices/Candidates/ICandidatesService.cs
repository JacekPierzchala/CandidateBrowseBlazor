using CandidateBrowserCleanArch.Blazor.WASM.StateContainers;
using CandidateBrowserCleanArch.Blazor.WASM.ViewModels;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

namespace CandidateBrowserCleanArch.Blazor.WASM.WebServices;

public interface ICandidatesService
{
    Task<Response<IEnumerable<CandidateListDto>>> GetActiveCandidatesAsync
        (CandidateSearchParameters searchParameters);
    Task<Response<CandidateDetailsDto>> GetCandidateDetailsAsync(int candidateId);
    Task<Response<CandidateDetailsDto>>UpdateCandidateMainInfoAsync(CandidateEditViewModel candidate);
    Task<Response<CandidateDetailsDto>> AddCandidateMainInfoAsync(CandidateEditViewModel candidate);
}
