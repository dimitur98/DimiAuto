﻿namespace DimiAuto.Models.CarModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
            this.Views += 1;
        }

        public TypeOfVeichle TypeOfVeichle { get; set; }

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

        public DateTime YearOfProduction { get; set; }

        public int Km { get; set; }

        public Doors Door { get; set; }

        public Color Color { get; set; }

        public EuroStandart EuroStandart { get; set; }

        public string Location { get; set; }

        public string MoreInformation { get; set; }

        public bool IsApproved { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Extras { get; set; }

        public string ImgsPaths { get; set; }

        public int Views { get; set; }
    }
}
