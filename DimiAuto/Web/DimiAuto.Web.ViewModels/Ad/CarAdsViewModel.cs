namespace DimiAuto.Web.ViewModels.Ad
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DimiAuto.Data.Models;
    using DimiAuto.Data.Models.CarModel;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class CarAdsViewModel : IMapFrom<Car>
    {
        public string Id { get; set; }

        public Make Make { get; set; }

        public string Model { get; set; }

        public string Modification { get; set; }

        public string MoreInformation { get; set; }

        public decimal Price { get; set; }

        public string YearOfProduction { get; set; }

        public Fuel Fuel { get; set; }

        public int Km { get; set; }

        public string ImgPath { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
