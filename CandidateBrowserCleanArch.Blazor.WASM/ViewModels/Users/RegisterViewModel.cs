using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CandidateBrowserCleanArch.Blazor.WASM.ViewModels;

public class RegisterViewModel
{
    [Required]
    [MinLength(5, ErrorMessage = $"{nameof(FirstName)} should have min 5 characters")]
    [MaxLength(20, ErrorMessage = $"{nameof(FirstName)} should have max 20 characters")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Required]
    [MinLength(5, ErrorMessage = $"{nameof(LastName)} should have min 5 characters")]
    [MaxLength(20, ErrorMessage = $"{nameof(LastName)} should have max 20 characters")]
    [Display(Name = "First Name")]
    public string LastName { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email adress format")]
    public string Email { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = $"{nameof(Password)} should have min 8 characters")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
    ErrorMessage = "Password must contain at least one uppercase letter, one digit, and one special character, and be at least 8 characters long.")]
    public string Password { get; set; }

    [Required]
    [Compare(nameof(Password),ErrorMessage =$"{nameof(ConfirmPassword)} must match {nameof(Password)}")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }
}