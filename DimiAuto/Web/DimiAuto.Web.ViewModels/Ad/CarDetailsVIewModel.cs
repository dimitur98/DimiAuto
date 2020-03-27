namespace DimiAuto.Web.ViewModels.Ad
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    using DimiAuto.Data.Models;
    using DimiAuto.Data.Models.CarModel;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Web.ViewModels.Ad.Comment;

    public class CarDetailsVIewModel
    {
        public string Id { get; set; }

        public Condition Condition { get; set; }

        public Make Make { get; set; }

        public string Model { get; set; }

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

        public string Location { get; set; }

        public string MoreInformation { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public IEnumerable<string> Extras { get; set; }

        public IEnumerable<string> ImgsPaths { get; set; }

        public int Views { get; set; }

        public IEnumerable<CarCommentViewModel> Comments { get; set; }
    }
}
