namespace DimiAuto.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using DimiAuto.Data.Common.Models;
    using DimiAuto.Models.CarModel;

    public class AdView : BaseDeletableModel<int>
    {
       

        public string CarId { get; set; }

        public string User { get; set; }

    }
}
