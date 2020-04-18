using System.ComponentModel.DataAnnotations;

namespace DimiAuto.Data.Models.CarModel
{
    public enum EuroStandart
    {
        All = 1,
        [Display(Name = "Euro 1")]
        Euro1 = 2,
        [Display(Name = "Euro 2")]
        Euro2 = 3,
        [Display(Name = "Euro 3")]
        Euro3 = 4,
        [Display(Name = "Euro 4")]
        Euro4 = 5,
        [Display(Name = "Euro 5")]
        Euro5 = 6,
        [Display(Name = "Euro 6")]
        Euro6 = 7,
    }
}
