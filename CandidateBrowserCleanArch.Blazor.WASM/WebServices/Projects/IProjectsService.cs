using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

namespace CandidateBrowserCleanArch.Blazor.WASM.WebServices;

public interface IProjectsService
{
    Task<Response<IEnumerable<ReadProjectDto>>> GetAllActiveProjectsAsync(bool overWrite);
}
