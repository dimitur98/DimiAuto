using DimiAuto.Data.Models.CarModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DimiAuto.Web.ViewModels
{
    public class SearchInputModel
    {
        public Make Make { get; set; }

        public string? Model { get; set; }

        public Condition Condition { get; set; }

        public Fuel Fuel { get; set; }

        public GearBox GearBox { get; set; }

        public int? YearFrom { get; set; }

        public int? YearTo { get; set; }

        public decimal? PriceFrom { get; set; }

        public decimal? PriceTo { get; set; }

        public TypeOfVeichle TypeOfVeichle { get; set; }
    }
}
