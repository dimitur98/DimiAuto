using DimiAuto.Data.Common.Models;
using DimiAuto.Models.CarModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DimiAuto.Data.Models
{
    public class AdView : BaseDeletableModel<int>
    {
        [Required]
        public string CarId { get; set; }

        [Required]
        public string User { get; set; }

    }
}
