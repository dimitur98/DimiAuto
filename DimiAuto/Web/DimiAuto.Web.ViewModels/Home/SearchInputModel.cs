﻿using DimiAuto.Common;
using DimiAuto.Data.Models;
using DimiAuto.Data.Models.CarModel;
using DimiAuto.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DimiAuto.Web.ViewModels.Home
{
    public class SearchInputModel : IMapFrom<SearchModel>
    {
        public Make Make { get; set; }

        public string? Model { get; set; }

        public Condition Condition { get; set; }

        public Fuel Fuel { get; set; }

        public GearBox GearBox { get; set; }

        [Range(GlobalConstants.YearRangeMin, GlobalConstants.YearRangeMax)]
        public int? YearFrom { get; set; }

        [Range(GlobalConstants.YearRangeMin, GlobalConstants.YearRangeMax)]
        public int? YearTo { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? PriceFrom { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? PriceTo { get; set; }

        public TypeOfVeichle TypeOfVeichle { get; set; }
    }
}
