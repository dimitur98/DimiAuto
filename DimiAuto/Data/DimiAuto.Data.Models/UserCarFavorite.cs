using DimiAuto.Data.Common.Models;
using DimiAuto.Models.CarModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DimiAuto.Data.Models
{
    public class UserCarFavorite : BaseDeletableModel<string>
    {
        public UserCarFavorite()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public string CarId { get; set; }
        public virtual Car Car { get; set; }
    }
}
