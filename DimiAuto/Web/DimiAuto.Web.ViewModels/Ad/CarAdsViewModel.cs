namespace DimiAuto.Web.ViewModels.Ad
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public string ModelToString { get; set; }

        public string Modification { get; set; }

        public string MoreInformation { get; set; }

        public decimal Price { get; set; }

        public DateTime YearOfProduction { get; set; }

        public Fuel Fuel { get; set; }

        public int Km { get; set; }

        public string ImgPath { get; set; }

        public Gearbox Gearbox { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public Condition Condition { get; set; }

        public TypeOfVeichle TypeOfVeichle { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
