namespace DimiAuto.Models.CarModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using DimiAuto.Common;
    using DimiAuto.Data.Common.Models;
    using DimiAuto.Data.Models;
    using DimiAuto.Data.Models.CarModel;

    public class Car : BaseDeletableModel<string>
    {
        public Car()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsApproved = false;
            this.IsDeleted = false;
            this.Comments = new HashSet<Comment>();

        }

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

        public GearBox Gearbox { get; set; }

        public Fuel Fuel { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Horsepowers { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Cc { get; set; }

        [Required]
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

        public bool IsApproved { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Extras { get; set; }

        public string ImgsPaths { get; set; }

        public int Views { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
