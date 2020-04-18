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
    using DimiAuto.Web;

    public class CreateAdInputModel
    {
        public string Id { get; set; }

        [CanNotChooseAll(ErrorMessage = "You can't choose 'All' for 'Type of vehicle' field!")]
        [Display(Name = "Type of vehicle")]
        public TypeOfVeichle TypeOfVeichle { get; set; }

        [CanNotChooseAll(ErrorMessage = "You can't choose 'All' for 'Condition' field!")]
        public Condition Condition { get; set; }

        [CanNotChooseAll(ErrorMessage = "You can't choose 'All' for 'Make' field!")]
        public Make Make { get; set; }

        [Required]
        [StringLength(GlobalConstants.CarModelLenght)]
        public string Model { get; set; }

        public string Modification { get; set; }

        [CanNotChooseAll(ErrorMessage = "You can't choose 'All' for 'Type' field!")]
        public Types Type { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [CanNotChooseAll(ErrorMessage = "You can't choose 'All' for 'Gearbox' field!")]
        public Gearbox Gearbox { get; set; }

        [CanNotChooseAll(ErrorMessage = "You can't choose 'All' for 'Fuel' field!")]
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

        [CanNotChooseAll(ErrorMessage = "You can't choose 'All' for 'Door' field!")]
        public Doors Door { get; set; }

        [CanNotChooseAll(ErrorMessage = "You can't choose 'All' for 'Color' field!")]
        public Color Color { get; set; }

        [CanNotChooseAll(ErrorMessage = "You can't choose 'All' for 'Euro standart' field!")]
        [Display(Name = "Euro standart")]
        public EuroStandart EuroStandart { get; set; }

        [CanNotChooseAll(ErrorMessage = "You can't choose 'All' for 'Location' field!")]
        public Location Location { get; set; }

        [Display(Name = "More information")]
        public string MoreInformation { get; set; }

        public string Extras { get; set; }

        public string ImgsPaths { get; set; }
    }
}
