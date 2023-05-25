using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public class CandidateProjectEditModel
{
    public int Id { get; set; }

    public int CandidateId { get; set; }

    [Required]
    [Display(Name = "Project")]
    public int? ProjectId { get; set; }
}
