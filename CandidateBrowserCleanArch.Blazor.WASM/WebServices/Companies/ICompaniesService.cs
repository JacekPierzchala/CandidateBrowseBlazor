using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

namespace CandidateBrowserCleanArch.Blazor.WASM.WebServices;

public interface ICompaniesService
{
    Task<Response<IEnumerable<ReadCompanyDto>>> GetAllActiveCompaniesAsync(bool overWrite);
}
