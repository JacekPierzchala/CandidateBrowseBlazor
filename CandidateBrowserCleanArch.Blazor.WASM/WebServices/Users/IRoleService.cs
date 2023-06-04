using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public interface IRoleService
{
    Task<Response<IEnumerable<RoleDto>>> GetRolesAsync();
}
