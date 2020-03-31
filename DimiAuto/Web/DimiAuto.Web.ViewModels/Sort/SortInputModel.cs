using DimiAuto.Data.Models;
using DimiAuto.Web.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Text;

namespace DimiAuto.Web.ViewModels.Sort
{
    public class SortInputModel
    {
        public string OrderByPrice { get; set; }

        public string OrderByYear { get; set; }

        public SearchInputModel SearchInputModel { get; set; }
    }
}
