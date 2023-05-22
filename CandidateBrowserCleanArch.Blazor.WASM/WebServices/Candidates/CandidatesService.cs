using AutoMapper;
using Blazored.LocalStorage;
using CandidateBrowserCleanArch.Blazor.WASM.StateContainers;
using CandidateBrowserCleanArch.Blazor.WASM.ViewModels;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;

namespace CandidateBrowserCleanArch.Blazor.WASM.WebServices;

public class CandidatesService : BaseHttpService, ICandidatesService
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IMapper _mapper;

    public CandidatesService(ICandidateBrowserWebAPIClient client,
        IAuthenticationService authenticationService,
        ITokenService tokenService,IConfiguration configuration,
        IMapper mapper
        ) : base(client, tokenService, configuration)
    {
        _authenticationService = authenticationService;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<CandidateListDto>>> GetActiveCandidatesAsync
        (CandidateSearchParameters searchParameters)
    {
        var response = new Response<IEnumerable<CandidateListDto>>();
        try
        {
            if(await GetBearerTokenAsync())
            {
                var resultApi = await _client.CandidatesGETAsync(searchParameters.FirstName,
                             searchParameters.LastName,
                             searchParameters.Companies?.Count() == 0 ? null : searchParameters.Companies,
                             searchParameters.Projects?.Count() == 0 ? null : searchParameters.Projects,
                             searchParameters.PageNumber, searchParameters.PageSize, 
                             ApiVersion);
                response.Success = true;
                response.Data = resultApi.Items;
                _mapper.Map(resultApi, searchParameters);
            }
            else
            {
                await _authenticationService.LogOut();
            }
        }

        catch (ApiException ex)
        {
            response =ConvertApiExceptions<IEnumerable<CandidateListDto>>(ex);
        }
        return response;
    }

    public async Task<Response<CandidateDetailsDto>> GetCandidateDetailsAsync(int candidateId)
    {
        var response = new Response<CandidateDetailsDto>();
        try
        {
            if (await GetBearerTokenAsync())
            {
                var resultApi = await _client.CandidatesGET2Async(candidateId, ApiVersion);
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
            response = ConvertApiExceptions<CandidateDetailsDto>(ex);
        }

        return response;
    }

    public async Task<Response<CandidateDetailsDto>> UpdateCandidateMainInfoAsync(CandidateEditViewModel candidate)
    {
        var response = new Response<CandidateDetailsDto>();
        try
        {
            if (await GetBearerTokenAsync())
            {

                var resultApi = await _client.CandidatesPUTAsync(candidate.Id, ApiVersion,_mapper.Map<CandidateUpdateDto>(candidate));
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
            response = ConvertApiExceptions<CandidateDetailsDto>(ex);
        }

        return response;
    }
}
