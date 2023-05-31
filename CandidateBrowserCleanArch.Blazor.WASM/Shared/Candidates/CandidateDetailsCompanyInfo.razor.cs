using AutoMapper;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;

namespace CandidateBrowserCleanArch.Blazor.WASM.Shared.Candidates;
public partial class CandidateDetailsCompanyInfo:ComponentBase
{
    [Inject] ICandidateCompanyService CandidateCompanyService { get; set; }
    [Inject] IMapper mapper { get; set; }
    [Inject] NotificationService NotificationService { get; set; }


    [Parameter]
    public ICollection<CandidateCompanyDto> Companies { get; set; }

    [Parameter]
    public int CandidateId { get; set; }

    bool companyDialogOpen;
    bool confirmationDialogOpen;
    string? confirmationMessage;
    int companyToDelete;
    CandidateCompanyEditViewModel company;

    private async Task OnCompanyDelete(CandidateCompanyDto company)
    {
        confirmationMessage = $"Are you sure to delete this company:{company.Company.CompanyName}?";
        companyToDelete = company.Id;
        confirmationDialogOpen = true;
    }

    private async Task OnDeleteConfirmationReceived(Tuple<bool, int> message)
    {
        if (message.Item1)
        {
            var result = await CandidateCompanyService.DeleteCandidateCompanyAsync(message.Item2);
            if (!result.Success)
            {
                NotificationService
                                .Notify(new NotificationMessage
                                {
                                    Severity = NotificationSeverity.Error,
                                    Duration = 4000,
                                    Summary = "Error",
                                    Detail = $"Update has failed:{result.Message}"
                                });

            }
            var company = Companies.FirstOrDefault(co => co.Id == message.Item2);
            if (company != null)
            {
                Companies.Remove(company);
            }
        }
        confirmationDialogOpen = false;
    }


    protected async override Task OnInitializedAsync()
    {
        if (Companies == null)
        {
            var response = await CandidateCompanyService.GetCompaniesByCandidateAsync(CandidateId);
            if (response.Success)
            {
                Companies = response.Data != null ? response.Data.ToList() : new List<CandidateCompanyDto>();
            }
            else
            {
                //handle
            }
        }
        Companies = Companies.OrderByDescending(c => c.DateStart).ToList();
    }

    private async Task OnCompanyClose()
    {
        companyDialogOpen = false;
    }

    private async Task OnCompanyOpen(CandidateCompanyDto? candidateCompany)
    {

        if (candidateCompany != null)
        {
            company = mapper.Map<CandidateCompanyEditViewModel>(candidateCompany);
        }
        else
        {
            company = new();
        }
        company.CandidateId = CandidateId;
        companyDialogOpen = true;
    }

    private async Task OnSaveCompanyAsync(CandidateCompanyEditViewModel model)
    {
        var result = await ProcessCompanyChange(model);
        if (result.result)
        {
            NotificationService
               .Notify(new NotificationMessage
               {
                   Severity = NotificationSeverity.Success,
                   Duration = 4000,
                   Summary = "Success",
                   Detail = $"Update has succeeded"
               });
        }
        else
        {
            NotificationService
               .Notify(new NotificationMessage
               {
                   Severity = NotificationSeverity.Error,
                   Duration = 4000,
                   Summary = "Error",
                   Detail = $"Update has failed:{result.message}"
               });
        }
        companyDialogOpen = false;
    }

    private async Task<(bool result, string message)>
    ProcessCompanyChange(CandidateCompanyEditViewModel model)
    {
        (bool result, string message) result = new();
        if (model.Id > 0)
        {
            var resultService = await CandidateCompanyService.UpdateCandidateCompanyAsync(model);
            if (resultService.Success)
            {
                var company = Companies.FirstOrDefault(co => co.Id == resultService.Data.Id);
                if (company != null)
                {
                    Companies.Remove(company);
                }
                Companies.Add(resultService.Data);
            }

            result.result = resultService.Success;
            result.message = resultService.Message;
        }
        else
        {
            var resultService = await CandidateCompanyService.AddCandidateCompanyAsync(model);
            if (resultService.Success)
            {
                Companies.Add(resultService.Data);
            }
            result.result = resultService.Success;
            result.message = resultService.Message;

        }
        Companies = Companies.OrderByDescending(c => c.DateStart).ToList();
        return result;
    }

}
