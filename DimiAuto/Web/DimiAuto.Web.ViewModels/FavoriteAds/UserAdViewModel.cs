using DimiAuto.Data.Models;
using DimiAuto.Models.CarModel;
using DimiAuto.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace DimiAuto.Web.ViewModels.FavoriteAds
{
    public class UserAdViewModel : IMapFrom<UserCarFavorite>
    {
        public string CarId { get; set; }

        public virtual Car Car { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
