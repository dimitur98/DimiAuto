using DimiAuto.Data.Models;
using DimiAuto.Web.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DimiAuto.Web.ViewModels.Sort
{
    public class SortInputModel
    {
        [Required]
        public string OrderByPrice { get; set; }

        [Required]
        public string OrderByYear { get; set; }

        public SearchInputModel SearchInputModel { get; set; }
    }
}
