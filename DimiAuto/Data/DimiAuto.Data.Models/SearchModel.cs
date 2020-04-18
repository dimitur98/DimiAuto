using DimiAuto.Data.Common.Models;
using DimiAuto.Data.Models.CarModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DimiAuto.Data.Models
{
   public class SearchModel : BaseDeletableModel<string>
    {
        public SearchModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Make Make { get; set; }


        public string Model { get; set; }

        public Condition Condition { get; set; }

        public Location Location { get; set; }

        public Fuel Fuel { get; set; }

        public GearBox GearBox { get; set; }

        public int? YearFrom { get; set; }

        public int? YearTo { get; set; }

        public decimal? PriceFrom { get; set; }

        public decimal? PriceTo { get; set; }

        public TypeOfVeichle TypeOfVeichle { get; set; }

        [Required]
        public string UserId { get; set; }

    }
}
