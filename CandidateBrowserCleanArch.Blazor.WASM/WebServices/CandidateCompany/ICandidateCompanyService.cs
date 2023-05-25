using CandidateBrowserCleanArch.Blazor.WASM.ViewModels;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public interface ICandidateCompanyService
{
    Task<Response<IEnumerable<CandidateCompanyDto>>> GetCompaniesByCandidateAsync(int candidateId);
    Task<Response<CandidateCompanyDto>> UpdateCandidateCompanyAsync(CandidateCompanyEditViewModel candidateCompany);
    Task<Response<CandidateCompanyDto>> AddCandidateCompanyAsync(CandidateCompanyEditViewModel candidateCompany);
    Task<Response<bool>> DeleteCandidateCompanyAsync(int id);
}
