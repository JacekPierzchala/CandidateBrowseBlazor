using CandidateBrowserCleanArch.Blazor.WASM.Shared.Candidates;
using CandidateBrowserCleanArch.Blazor.WASM.StateContainers;
using CandidateBrowserCleanArch.Blazor.WASM.Statics;
using CandidateBrowserCleanArch.Blazor.WASM.ViewModels;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace CandidateBrowserCleanArch.Blazor.WASM.Pages;

public partial class Index:ComponentBase
{
    [Inject] DialogService DialogService { get; set; }
    [Inject]
    CandidateSearchStateContainer CandidateSearchStateContainer { get; set; }
    [Inject] NotificationService NotificationService { get; set; }

    bool IsLoadDataChanged;
    bool IsFiltered;
    bool AddCandidateDialogOpen;
    CandidateEditViewModel candidateToAdd;

    public async Task OpenOrder()
    {
        await DialogService.OpenSideAsync<CandidatesSearchPanel>("Candidate search filters", null,
         new SideDialogOptions
         {
             CloseDialogOnOverlayClick = false,
             Position = DialogPosition.Left,
             ShowMask = true,
             ShowTitle = true,
             Height = "600px;",
             Width = "400px;"
         });
    }

    private async Task OnLoadDataChanged(Tuple<bool, bool> args)
    {
        IsLoadDataChanged = args.Item1;
        IsFiltered = args.Item2;
        StateHasChanged();
    }

    public async Task AddCandidateAsync()
    {
        candidateToAdd = new();
        AddCandidateDialogOpen = true;
    }

    private async Task OnCloseAsync(CandidateEditViewModel candidate)
    {
        if (candidate.Id > 0)
        {


            CandidateSearchStateContainer.ClearSearchParameters();
            CandidateSearchStateContainer.SearchTrigerred?.Invoke();

            NotificationService
          .Notify(new NotificationMessage
          {
              Severity = NotificationSeverity.Success,
              Duration = 4000,
              Summary = "Success",
              Detail = $"{candidate.FirstName} {candidate.LastName} created"
          });
        }

        AddCandidateDialogOpen = false;
    }

}
