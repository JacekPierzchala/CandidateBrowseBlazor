using System.ComponentModel.DataAnnotations;

namespace CandidateBrowserCleanArch.Blazor.WASM.ViewModels;

public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email adress format")]
    public string Email { get; set; }

    public string ReturnUrl { get; set; }
}
