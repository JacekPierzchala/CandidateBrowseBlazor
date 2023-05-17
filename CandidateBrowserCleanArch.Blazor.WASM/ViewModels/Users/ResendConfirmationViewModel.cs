using System.ComponentModel.DataAnnotations;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public class ResendConfirmationViewModel
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email adress format")]
    public string Email { get; set; }

    public string ReturnUrl { get; set; }
}
