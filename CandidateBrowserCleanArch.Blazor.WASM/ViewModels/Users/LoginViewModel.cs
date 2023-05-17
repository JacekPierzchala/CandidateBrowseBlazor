using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CandidateBrowserCleanArch.Blazor.WASM.ViewModels;

public class LoginViewModel
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email adress format")]
    public string Email { get; set; }

    [Required]
    [MinLength(8,ErrorMessage ="Password should have min 8 characters")]

    public string Password { get; set; }
}
