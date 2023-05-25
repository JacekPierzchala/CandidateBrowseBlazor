using AutoMapper;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public class CandidateProjectService : BaseHttpService, ICandidateProjectService
{
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _authenticationService;

    public CandidateProjectService(ICandidateBrowserWebAPIClient client, 
        ITokenService tokenService, 
        IConfiguration configuration, 
        IMapper mapper, 
        IAuthenticationService authenticationService) : 
        base(client, tokenService, configuration)
    {
        _mapper = mapper;
        _authenticationService = authenticationService;
    }

    public async Task<Response<CandidateProjectDto>> AddCandidateProjectAsync(CandidateProjectEditModel candidateProject)
    {
        var response = new Response<CandidateProjectDto>();
        try
        {
            if (await GetBearerTokenAsync())
            {
                var resultApi = await _client.CandidateProjectPOSTAsync(ApiVersion, _mapper.Map<CandidateProjectAddDto>(candidateProject));
                response.Success = resultApi.Success;
                response.Data = resultApi.Data;
            }
            else
            {
                await _authenticationService.LogOut();
            }
        }
        catch (ApiException ex)
        {
            response = ConvertApiExceptions<CandidateProjectDto>(ex);
        }
        return response;
    }

    public async Task<Response<bool>> DeleteCandidateProjectAsync(int id)
    {
        var response = new Response<bool>();
        try
        {
            if (await GetBearerTokenAsync())
            {
                var resultApi = await _client.CandidateProjectDELETEAsync(id, ApiVersion);
                response.Success = resultApi.Success;
                response.Data = resultApi.Data;
            }
            else
            {
                await _authenticationService.LogOut();
            }
        }
        catch (ApiException ex)
        {
            response = ConvertApiExceptions<bool>(ex);
        }
        return response;
    }

    public async Task<Response<IEnumerable<CandidateProjectDto>>> GetProjectsByCandidateAsync(int candidateId)
    {
        var response = new Response<IEnumerable<CandidateProjectDto>>();
        try
        {
            if (await GetBearerTokenAsync())
            {
                var resultApi = await _client.Candidate2Async(candidateId, ApiVersion);
                response.Success = resultApi.Success;
                response.Data = resultApi.Data;
            }
            else
            {
                await _authenticationService.LogOut();
            }
        }
        catch (ApiException ex)
        {
            response = ConvertApiExceptions<IEnumerable<CandidateProjectDto>>(ex);
        }
        return response;
    }

    public async Task<Response<CandidateProjectDto>> UpdateCandidateProjectAsync(CandidateProjectEditModel candidateProject)
    {
        var response = new Response<CandidateProjectDto>();
        try
        {
            if (await GetBearerTokenAsync())
            {
                var resultApi = await _client
                    .CandidateProjectPUTAsync(candidateProject.Id, ApiVersion, _mapper.Map<CandidateProjectUpdateDto>(candidateProject));
                response.Success = resultApi.Success;
                response.Data = resultApi.Data;
            }
            else
            {
                await _authenticationService.LogOut();
            }
        }
        catch (ApiException ex)
        {
            response = ConvertApiExceptions<CandidateProjectDto>(ex);
        }
        return response;
    }
}
