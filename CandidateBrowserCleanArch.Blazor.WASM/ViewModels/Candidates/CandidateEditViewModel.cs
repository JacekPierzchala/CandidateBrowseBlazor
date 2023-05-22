using System.ComponentModel.DataAnnotations;

namespace CandidateBrowserCleanArch.Blazor.WASM.ViewModels;

public class CandidateEditViewModel
{
    public int Id { get; set; }

    [Required]
    [MinLength(5, ErrorMessage = $"{nameof(FirstName)} should have min 5 characters")]
    [MaxLength(20, ErrorMessage = $"{nameof(FirstName)} should have max 20 characters")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Required]
    [MinLength(5, ErrorMessage = $"{nameof(LastName)} should have min 5 characters")]
    [MaxLength(20, ErrorMessage = $"{nameof(LastName)} should have max 20 characters")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email adress format")]
    public string Email { get; set; }

    [DataType(DataType.Date),Required,CandidateDateOfBirthValidation]
    public DateTime DateOfBirth { get; set; }
    public string Description { get; set; }
    public string? ProfilePicture { get; set; }
    public string? ProfilePictureOld { get; set; }
    public string? ProfilePath { get; set; }

    public string? ProfilePictureData { get; set; }

}
