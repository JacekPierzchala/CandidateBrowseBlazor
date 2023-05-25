using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public interface ICandidateProjectService
{
    Task<Response<IEnumerable<CandidateProjectDto>>> GetProjectsByCandidateAsync(int candidateId);
    Task<Response<CandidateProjectDto>> UpdateCandidateProjectAsync(CandidateProjectEditModel candidateProject);
    Task<Response<CandidateProjectDto>> AddCandidateProjectAsync(CandidateProjectEditModel candidateProject);
    Task<Response<bool>> DeleteCandidateProjectAsync(int id);
}
