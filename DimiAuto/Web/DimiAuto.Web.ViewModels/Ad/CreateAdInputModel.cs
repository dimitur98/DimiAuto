using DimiAuto.Data.Models.CarModel;
using DimiAuto.Services.Mapping;
using FinalProject.Models.CarModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DimiAuto.Web.ViewModels.Ad
{
   public class CreateAdInputModel 
    {
        public string Id { get; set; }

        public TypeOfVeichle TypeOfVeichle { get; set; }

        public TypesOfAd TypeOfAd { get; set; }

        public Condition Condition { get; set; }

        public Make Make { get; set; }

        public string Model { get; set; }

        public string Modification { get; set; }

        public Types Type { get; set; }

        public decimal Price { get; set; }

        public GearBox GearBox { get; set; }

        public Fuel Fuel { get; set; }

        public int Hp { get; set; }

        public int Cc { get; set; }

        [DataType(DataType.Date)]
        public DateTime YearOfProduction { get; set; }

        public int Km { get; set; }

        public Doors Door { get; set; }

        public Color Color { get; set; }

        public EuroStandart EuroStandart { get; set; }

        public string Location { get; set; }

        public string MoreInformation { get; set; }


        public string Extras { get; set; }

        public string ImgsPaths { get; set; }
    }
}
