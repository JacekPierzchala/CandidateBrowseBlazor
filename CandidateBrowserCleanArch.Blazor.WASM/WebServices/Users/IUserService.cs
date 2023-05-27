using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public interface IUserService
{
    Task<Response<IEnumerable<ReadUserListDto>>> GetUsersAsync(CancellationToken cancellationToken = default);
    Task<Response<ReadUserListDto>> GetUsersDetailsAsync(string userId);
}
