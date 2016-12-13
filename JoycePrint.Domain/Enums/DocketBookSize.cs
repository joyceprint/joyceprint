using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace JoycePrint.Domain.Enums
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum DocketBookSize
    {
        [Display(Name = "A4")]
        A4 = 0,
        [Display(Name = "A5")]
        A5,
        [Display(Name = "A6")]
        A6,
        [Display(Name = "DL")]
        DL
    }
}