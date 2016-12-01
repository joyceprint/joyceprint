using System.ComponentModel.DataAnnotations;

namespace JoycePrint.Domain.Enums
{
    public enum DocketBookSize
    {
        [Display(Name = "A4")]
        A4,
        [Display(Name = "A5")]
        A5,
        [Display(Name = "A6")]
        A6,
        [Display(Name = "DL")]
        DL
    }
}