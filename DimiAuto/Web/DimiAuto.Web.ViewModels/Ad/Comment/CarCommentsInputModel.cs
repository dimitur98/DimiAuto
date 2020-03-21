using DimiAuto.Data.Models;
using DimiAuto.Models.CarModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DimiAuto.Web.ViewModels.Ad
{
    public class CarCommentsInputModel
    {

        public string Content { get; set; }

        public string UserId { get; set; }

        public string CarId { get; set; }

        public string Title { get; set; }
    }
}
