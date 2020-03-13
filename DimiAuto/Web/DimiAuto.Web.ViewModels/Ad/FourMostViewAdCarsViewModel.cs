namespace DimiAuto.Web.ViewModels.Ad
{
    using DimiAuto.Data.Models.CarModel;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Mapping;

    public class FourMostViewAdCarsViewModel : IMapFrom<Car>
    {
        public string Id { get; set; }

        public decimal Price { get; set; }

        public Make Make { get; set; }

        public Fuel Fuel { get; set; }

        public string Model { get; set; }

        public string Modification { get; set; }

        public string Year { get; set; }

        public string UserId { get; set; }

        public string ImgPad { get; set; }
    }
}
