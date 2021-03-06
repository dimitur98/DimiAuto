﻿namespace DimiAuto.Web.ViewModels.Ad
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

    public class CarDetailsViewModel
    {
        public string Id { get; set; }

        public Condition Condition { get; set; }

        public Make Make { get; set; }

        public string Model { get; set; }

        public string ModelToString { get; set; }

        public string Modification { get; set; }

        public Types Type { get; set; }

        public decimal Price { get; set; }

        public Gearbox Gearbox { get; set; }

        public Fuel Fuel { get; set; }

        public int Horsepowers { get; set; }

        public int Cc { get; set; }


        public string YearOfProduction { get; set; }

        public int Km { get; set; }

        public Doors Door { get; set; }

        public Color Color { get; set; }

        public EuroStandart EuroStandart { get; set; }

        public Location Location { get; set; }

        public string MoreInformation { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public IEnumerable<string> Extras { get; set; }

        public IEnumerable<string> ImgsPaths { get; set; }

        public int Views { get; set; }

        public IEnumerable<CarCommentViewModel> Comments { get; set; }

        public bool IsApproved { get; set; }

        public bool IsDeleted { get; set; }

        public string CurrentUserId { get; set; }

        public string Statuse
        {
            get
            {
                if (this.IsApproved)
                {
                    if (this.IsDeleted)
                    {
                        return "Approved, Deleted";

                    }
                    else
                    {
                        return "Approved, Not deleted";

                    }
                }
                else
                {
                    return "Not approved";
                }


            }
        }
    }
}
