using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public class EditUserViewModel
{
    public string Id { get; set; }

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
    [MinLength(1,ErrorMessage ="At least one role must be assigned")]
    public List<string> UserRoles { get; set; } = new();
    public List<RoleDto> Roles { get; set; } = new();
}
