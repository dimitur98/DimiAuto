using DimiAuto.Data.Models;
using DimiAuto.Data.Models.CarModel;
using DimiAuto.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace DimiAuto.Web.ViewModels.SearchHistory
{
    public class SearchViewModel : IMapFrom<SearchModel>
    {
        public string Id { get; set; }

        public Make Make { get; set; }

        public string Model { get; set; }

        public string ModelToString { get; set; }

        public Location Location { get; set; }

        public Condition Condition { get; set; }

        public Fuel Fuel { get; set; }

        public Gearbox Gearbox { get; set; }

        public int? YearFrom { get; set; }

        public int? YearTo { get; set; }

        public decimal? PriceFrom { get; set; }

        public decimal? PriceTo { get; set; }

        public TypeOfVeichle TypeOfVeichle { get; set; }
    }
}
