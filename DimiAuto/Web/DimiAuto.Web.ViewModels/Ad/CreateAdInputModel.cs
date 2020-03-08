using DimiAuto.Data.Models.CarModel;
using FinalProject.Models.CarModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DimiAuto.Web.ViewModels.Ad
{
   public class CreateAdInputModel
    {
        public string Section { get; set; }

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

        public DateTime YearOfProduction { get; set; }

        public int Km { get; set; }

        public string Doors { get; set; }

        public string Color { get; set; }

        public EuroStandart EuroStandart { get; set; }

        public string Location { get; set; }

        public string MoreInformation { get; set; }


        public IEnumerable<string> Extras { get; set; }
        // ection,for,state,make,model,modification,type,price,gearbox,fuel,hp,cc,year,km,doors,color,euroStandart,isInBg,moreInformation,extras
    }
}
