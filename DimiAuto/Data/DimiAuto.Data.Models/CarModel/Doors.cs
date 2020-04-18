using System.ComponentModel.DataAnnotations;

namespace DimiAuto.Data.Models.CarModel
{

    public enum Doors
    {
        All = 1,
        [Display(Name = "2/3")]
        Three = 2,
        [Display(Name = "4/5")]
        Five = 3,
    }
}
