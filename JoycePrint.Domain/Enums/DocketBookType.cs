using System.ComponentModel.DataAnnotations;

namespace JoycePrint.Domain.Enums
{
    public enum DocketBookType
    {
        [Display(Name = "Duplicate")]
        Duplicate,
        [Display(Name = "Triplicate")]
        Triplicate,
        [Display(Name = "Quad")]
        Quad
    }
}