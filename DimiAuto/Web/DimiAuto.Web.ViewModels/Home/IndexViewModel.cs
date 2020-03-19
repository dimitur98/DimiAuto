namespace DimiAuto.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using DimiAuto.Data.Models.CarModel;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Web.ViewModels.Ad;

    public class IndexViewModel
    {
        public Make Make { get; set; }

        public string Model { get; set; }

        public Condition Condition { get; set; }

        public string Location { get; set; }

        public string YearOfProduction { get; set; }

        public decimal Price { get; set; }
    }
}
