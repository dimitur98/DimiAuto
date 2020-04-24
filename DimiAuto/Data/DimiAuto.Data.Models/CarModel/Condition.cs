namespace DimiAuto.Data.Models.CarModel
{
    using System.ComponentModel.DataAnnotations;

    public enum Condition
    {
        All = 1,
        New = 2,
        [Display(Name = "For parts")]
        ForParts = 3,
        [Display(Name = "With problem")]
        WithProblem = 4,
    }
}
