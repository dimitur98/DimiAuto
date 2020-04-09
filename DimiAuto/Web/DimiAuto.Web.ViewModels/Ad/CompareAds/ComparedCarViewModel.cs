namespace DimiAuto.Web.ViewModels.Ad.CompareAds
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using DimiAuto.Data.Models.CarModel;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Mapping;

    public class ComparedCarViewModel : IMapFrom<Car>
    {
       

        public Condition Condition { get; set; }

        public Make Make { get; set; }

        public string Model { get; set; }

        public string ModelToString { get; set; }

        public string Modification { get; set; }

        public Types Type { get; set; }

        public decimal Price { get; set; }

        public GearBox Gearbox { get; set; }

        public Fuel Fuel { get; set; }

        public int Horsepowers { get; set; }

        public int Cc { get; set; }


        public string YearOfProduction { get; set; }

        public int Km { get; set; }

        public Doors Door { get; set; }

        public Color Color { get; set; }

        public EuroStandart EuroStandart { get; set; }

        public IEnumerable<string> Extras { get; set; }

        public string ImgPath { get; set; }

    }
}
