using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CandidateBrowserCleanArch.Blazor.WASM.ViewModels;

public class ResetPasswordViewModel
{
    public string UserId { get; set; }
    public string Token { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = $"{nameof(NewPassword)} should have min 8 characters")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
    ErrorMessage = "Password must contain at least one uppercase letter, one digit, and one special character, and be at least 8 characters long.")]
    public string NewPassword { get; set; }

    [Required]
    [Compare(nameof(NewPassword), ErrorMessage = $"{nameof(ConfirmNewPassword)} must match {nameof(NewPassword)}")]
    [Display(Name = "Confirm Password")]
    public string ConfirmNewPassword { get; set; }
}
