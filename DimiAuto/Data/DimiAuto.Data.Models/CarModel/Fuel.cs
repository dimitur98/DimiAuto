using System.ComponentModel.DataAnnotations;

namespace DimiAuto.Data.Models.CarModel
{
    public enum Fuel
    {
        All = 1,
        Gasoline = 2,
        Diesel = 3,
        [Display(Name = "Gas/Gasoline")]
        GasGasoline = 4,
        [Display(Name = "Methane/Gasoline")]
        MethaneGasoline = 5,
        Hybrid = 6,
        Electricity = 7,
    }
}