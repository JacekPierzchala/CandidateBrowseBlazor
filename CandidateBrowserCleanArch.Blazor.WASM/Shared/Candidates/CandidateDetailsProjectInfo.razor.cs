using AutoMapper;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace CandidateBrowserCleanArch.Blazor.WASM.Shared.Candidates;
public partial class CandidateDetailsProjectInfo:ComponentBase
{
    [Inject] ICandidateProjectService CandidateProjectService { get; set; }
    [Inject]
    IMapper mapper { get; set; }
    [Inject] NotificationService NotificationService { get; set; }

    [Parameter]
    public ICollection<CandidateProjectDto> Projects { get; set; }
    
    [Parameter]
    public int CandidateId { get; set; }

    int projectToDelete;
    CandidateProjectEditModel project;
    bool projectDialogOpen;
    bool confirmationDialogOpen;
    string? confirmationMessage;

    protected async override Task OnInitializedAsync()
    {
        if (Projects == null)
        {
            var response = await CandidateProjectService.GetProjectsByCandidateAsync(CandidateId);
            if (response.Success)
            {
                Projects = response.Data != null ? response.Data.ToList() : new List<CandidateProjectDto>();
            }
            else
            {
                //handle
            }
            Projects = Projects.OrderBy(c => c.Project.ProjectName).ToList();
        }

    }
    private async Task OnProjectClose()
    {
        projectDialogOpen = false;
    }

    private async Task OnProjectOpen(CandidateProjectDto? candidateProject)
    {

        if (candidateProject != null)
        {
            project = mapper.Map<CandidateProjectEditModel>(candidateProject);
        }
        else
        {
            project = new();
        }
        project.CandidateId = CandidateId;
        projectDialogOpen = true;
    }

    private async Task OnCompanyDelete(CandidateProjectDto project)
    {
        confirmationMessage = $"Are you sure to delete this company:{project.Project.ProjectName}?";
        projectToDelete = project.Id;
        confirmationDialogOpen = true;

    }

    private async Task OnSaveProjectAsync(CandidateProjectEditModel model)
    {
        var result = await ProcessProjectChange(model);
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
        projectDialogOpen = false;
    }

    private async Task OnDeleteConfirmationReceived(Tuple<bool, int> message)
    {
        if (message.Item1)
        {
            var result = await CandidateProjectService.DeleteCandidateProjectAsync(message.Item2);
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
            var candidateProject = Projects.FirstOrDefault(co => co.Id == message.Item2);
            if (candidateProject != null)
            {
                Projects.Remove(candidateProject);
            }
        }
        confirmationDialogOpen = false;
    }

    private async Task<(bool result, string message)>
    ProcessProjectChange(CandidateProjectEditModel model)
    {
        (bool result, string message) result = new();
        if (model.Id > 0)
        {
            var resultService = await CandidateProjectService.UpdateCandidateProjectAsync(model);
            if (resultService.Success)
            {
                var candidateProjectDto = Projects.FirstOrDefault(co => co.Id == resultService.Data.Id);
                if (candidateProjectDto != null)
                {
                    Projects.Remove(candidateProjectDto);
                }
                Projects.Add(resultService.Data);
            }

            result.result = resultService.Success;
            result.message = resultService.Message;
        }
        else
        {
            var resultService = await CandidateProjectService.AddCandidateProjectAsync(model);
            if (resultService.Success)
            {
                Projects.Add(resultService.Data);
            }
            result.result = resultService.Success;
            result.message = resultService.Message;

        }
        Projects = Projects.OrderBy(c => c.Project.ProjectName).ToList();
        return result;
    }
}
