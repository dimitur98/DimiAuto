namespace DimiAuto.Web.ViewModels.Ad
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using DimiAuto.Common;
    using DimiAuto.Data.Models.CarModel;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Web.ViewModels.Attribute;
    using FinalProject.Models.CarModel;

    public class CreateAdInputModel
    {
        public string Id { get; set; }

        [Display(Name = "Type of vehicle")]
        public TypeOfVeichle TypeOfVeichle { get; set; }

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
        [YearOfProductionValidate(ErrorMessage = "Year of production should be in format 'mm.yyyy' and should be valid!")]
        [MaxLength(GlobalConstants.YearOfProductionMaxLenght)]
        [Display(Name = "Year of production")]
        public string YearOfProduction { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Km { get; set; }

        public Doors Door { get; set; }

        public Color Color { get; set; }

        [Display(Name = "Euro standart")]
        public EuroStandart EuroStandart { get; set; }

        
        public Location Location { get; set; }

        [Display(Name = "More information")]
        public string MoreInformation { get; set; }

        public string Extras { get; set; }

        public string ImgsPaths { get; set; }
    }
}
