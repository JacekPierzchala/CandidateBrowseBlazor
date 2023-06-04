using CandidateBrowserCleanArch.Blazor.WASM.ViewModels;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public interface IUserService
{
    Task<Response<IEnumerable<ReadUserListDto>>> GetUsersAsync(CancellationToken cancellationToken = default);
    Task<Response<ReadUserDetailsDto>> GetUsersDetailsAsync(string userId);
    Task<Response<bool>> UpdateUserAsync(EditUserViewModel model);
}
