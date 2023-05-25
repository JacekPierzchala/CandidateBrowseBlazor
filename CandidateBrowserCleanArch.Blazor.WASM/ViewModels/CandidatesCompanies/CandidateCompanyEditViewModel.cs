using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public class CandidateCompanyEditViewModel: IValidatableObject
{
    public int Id { get; set; }

    public int CandidateId { get; set; }

    [Required]
    [Display(Name ="Company")]
    public int? CompanyId { get; set; }

    [Display(Name = "Start Date"), DataType(DataType.Date), Required]
    public DateTime DateStart { get; set; }=DateTime.Now;

    [Required]
    public string Position { get; set; }

    [Display(Name = "End Date"), DataType(DataType.Date)]
    public DateTime? DateEnd { get; set; } = DateTime.Now;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();
        if (DateEnd.HasValue && DateEnd.Value.Date < DateStart.Date)
        {
            errors.Add(new ValidationResult($"End Date needs to be greater than Start Date.", new List<string> { nameof(DateStart) }));
        }
        return errors;
    }
}
