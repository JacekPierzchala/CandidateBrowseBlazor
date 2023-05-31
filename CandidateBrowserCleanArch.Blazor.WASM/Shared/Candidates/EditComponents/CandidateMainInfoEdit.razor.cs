using CandidateBrowserCleanArch.Blazor.WASM.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CandidateBrowserCleanArch.Blazor.WASM.Shared.Candidates.EditComponents;
public partial class CandidateMainInfoEdit : ComponentBase
{
    [Parameter]
    public CandidateEditViewModel Candidate { get; set; }

    private string UploadFileWarning = string.Empty;
    private long maxFileSize = 1024 * 1024 * 3;

    [Parameter]
    public EventCallback<CandidateEditViewModel> SaveCandidateAsync { get; set; }
    private async Task HandlePhotoChange(InputFileChangeEventArgs e)
    {
        var file = e.File;
        if (e.File != null)
        {
            if (file.Size > maxFileSize)
            {
                UploadFileWarning = "This file is too big to upload";
                return;
            }
            string extension = System.IO.Path.GetExtension(file.Name);
            if (extension.ToLower().Contains("jpg") || extension.ToLower().Contains("png") || extension.ToLower().Contains("jpeg"))
            {

                var buffer = new byte[file.Size];
                var fileContent = new StreamContent(file.OpenReadStream(1024 * 1024 * 15));
                buffer = await fileContent.ReadAsByteArrayAsync();
                string base64String = Convert.ToBase64String(buffer);
                string imageType = file.ContentType;
                Candidate.ProfilePictureData = base64String;
                Candidate.ProfilePicture = file.Name;
                Candidate.ProfilePath = $"data:{imageType}; base64, {base64String}";
            }
            else
            {
                UploadFileWarning = "Please select a valid image file (*.jpg | *.png | *.jpeg)";
            }
        }

    }
    private async Task OnSaveCandidateAsync()
    {
        await SaveCandidateAsync.InvokeAsync(Candidate);
    }
}
