using AutoMapper;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public class CandidateCompanyService : BaseHttpService, ICandidateCompanyService
{
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _authenticationService;

    public CandidateCompanyService(ICandidateBrowserWebAPIClient client,
        ITokenService tokenService, IConfiguration configuration,
        IMapper mapper, IAuthenticationService authenticationService) : 
        base(client, tokenService, configuration)
    {
        _mapper = mapper;
        _authenticationService = authenticationService;
    }

    public async Task<Response<CandidateCompanyDto>> AddCandidateCompanyAsync(CandidateCompanyEditViewModel candidateCompany)
    {
        var response = new Response<CandidateCompanyDto>();
        try
        {
            if (await GetBearerTokenAsync())
            {
                var resultApi = await _client.CandidateCompanyPOSTAsync(ApiVersion, _mapper.Map<CandidateCompanyAddDto>(candidateCompany));
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
            response = ConvertApiExceptions<CandidateCompanyDto>(ex);
        }
        return response;
    }

    public async Task<Response<bool>> DeleteCandidateCompanyAsync(int id)
    {
        var response = new Response<bool>();
        try
        {
            if (await GetBearerTokenAsync())
            {
                var resultApi = await _client.CandidateCompanyDELETEAsync(id,ApiVersion);
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

    public async Task<Response<IEnumerable<CandidateCompanyDto>>> GetCompaniesByCandidateAsync(int candidateId)
    {
        var response = new Response<IEnumerable<CandidateCompanyDto>>();
        try
        {
            if (await GetBearerTokenAsync())
            {
                var resultApi = await _client.CandidateAsync(candidateId,ApiVersion);
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
            response = ConvertApiExceptions<IEnumerable<CandidateCompanyDto>>(ex);
        }
        return response;
    }

    public async Task<Response<CandidateCompanyDto>> UpdateCandidateCompanyAsync(CandidateCompanyEditViewModel candidateCompany)
    {
        var response = new Response<CandidateCompanyDto>();
        try
        {
            if (await GetBearerTokenAsync())
            {
                var resultApi = await _client.CandidateCompanyPUTAsync(candidateCompany.Id, ApiVersion, _mapper.Map<CandidateCompanyUpdateDto>(candidateCompany));
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
            response = ConvertApiExceptions<CandidateCompanyDto>(ex);
        }
        return response;
    }
}
