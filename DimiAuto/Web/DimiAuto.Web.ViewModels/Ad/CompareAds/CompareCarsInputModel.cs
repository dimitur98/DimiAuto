using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DimiAuto.Web.ViewModels.Ad.CompareAds
{
    public class CompareCarsInputModel
    {
        [Required]
        public string FirstCarId { get; set; }

        [Required]
        public string SecondCarId { get; set; }
    }
}
