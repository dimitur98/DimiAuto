using DimiAuto.Data.Common.Models;
using DimiAuto.Models.CarModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DimiAuto.Data.Models
{
    public class AdView : BaseDeletableModel<int>
    {
        public string CarId { get; set; }
        public virtual Car Car { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
