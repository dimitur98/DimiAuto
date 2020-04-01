using DimiAuto.Common;
using DimiAuto.Data.Models.CarModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DimiAuto.Web.ViewModels.Ad
{
   public class EditAddInputModel
    {
        [Required]
        public string Id { get; set; }

        public TypeOfVeichle TypeOfVeichle { get; set; }

        public Condition TypeOfAd { get; set; }

        public Condition Condition { get; set; }

        public Make Make { get; set; }

        [Required]
        [StringLength(GlobalConstants.CarModelLenght)]
        public string Model { get; set; }

        public string Modification { get; set; }

        public Types Type { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public GearBox GearBox { get; set; }

        public Fuel Fuel { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Hp { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Cc { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime YearOfProduction { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Km { get; set; }

        public Doors Door { get; set; }

        public Color Color { get; set; }

        public EuroStandart EuroStandart { get; set; }

        [Required]
        [StringLength(GlobalConstants.CarLocationLenght)]
        public string Location { get; set; }

        public string MoreInformation { get; set; }

        public string Extras { get; set; }

        public string ImgsPaths { get; set; }
    }
}
