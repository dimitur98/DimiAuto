namespace DimiAuto.Web.ViewModels.Ad
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using DimiAuto.Data.Models.CarModel;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Mapping;
    public class CarAdsViewModel : IMapFrom<Car>
    {
        public string Id { get; set; }

        public decimal Price { get; set; }

        public string Extras { get; set; }

        public Make Make { get; set; }

        public Fuel Fuel { get; set; }

        public string Model { get; set; }

        public TypeOfVeichle TypeOfVeichle { get; set; }

        public string Modification { get; set; }

        
        public string Year { get; set; }

        public string UserId { get; set; }

        public string ImgPad { get; set; }

    }
}
