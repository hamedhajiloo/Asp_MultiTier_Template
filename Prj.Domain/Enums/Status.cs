using System.ComponentModel.DataAnnotations;

namespace Prj.Domain.Enums
{
    public enum Status
    {
        [Display(Name = "در انتظار بررسی")]
        WaitingToReview = 1,

        [Display(Name = "تایید شده")]
        Accepted = 2,

        [Display(Name = "عدم تایید")]
        Recjected = 3
    }
}
